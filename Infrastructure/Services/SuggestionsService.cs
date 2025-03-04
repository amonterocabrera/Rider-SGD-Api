using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.Exceptions;
using SGDPEDIDOS.Application.Interfaces.Repositories;
using SGDPEDIDOS.Application.Interfaces.Services;
using SGDPEDIDOS.Application.Wrappers;
using SGDPEDIDOS.Domain.Entities;
using SGDPEDIDOS.Domain.Entities.View;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Infrastructure.Services
{
    public class SuggestionsService : BaseService, ISuggestionsService
    {
        private readonly IRepositoryAsync<Suggestion> _SuggestionRepo;
        private readonly IRepositoryAsync<VReportSuggestion> _VReportSuggestionRepo;
        private readonly IRepositoryAsync<User> _UserRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<SuggestionsDto> _validator;
        public SuggestionsService(IRepositoryAsync<Suggestion> SuggestionRepo, IRepositoryAsync<VReportSuggestion> VReportSuggestionRepo, IRepositoryAsync<User> user, IMapper mapper, IValidator<SuggestionsDto> validator, IHttpContextAccessor context) : base(context)
        {
            _SuggestionRepo = SuggestionRepo;
            _VReportSuggestionRepo = VReportSuggestionRepo;
            _UserRepo = user;
            _mapper = mapper;
            _validator = validator;
        }

        private async Task<bool> ExitsAsync(int Id)
        {
            var result = await _SuggestionRepo.Exists(x => x.MenuId.Equals(Id));
            if (result) { return true; }
            throw new ApiException("Suggestions not found");
        }

        public async Task<Response<SuggestionsVm>> InsertAsync(SuggestionsDto dto)
        {

            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            var obj = _mapper.Map<Suggestion>(dto);           
            obj.IsActive = true;
            obj.SuggestionDate = DateTime.UtcNow;
            obj.CreationDate = DateTime.UtcNow;
            obj.CreatedBy =  base.GetLoggerUserId();    

            return new Response<SuggestionsVm>(_mapper.Map<SuggestionsVm>(await _SuggestionRepo.AddAsync(obj)));

        }

        public async Task<Response<SuggestionsVm>> UpdateAsync(int id, SuggestionsDto dto)
        {
            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            await ExitsAsync(id);

            var objDb = await _SuggestionRepo.WhereAsync(x => x.MenuId.Equals(id));
            var obj = _mapper.Map<Suggestion>(dto);

            obj.MenuId = objDb.MenuId;
            obj.CreatedBy = objDb.CreatedBy;
            obj.CreationDate = objDb.CreationDate;
            obj.ModifiedBy = base.GetLoggerUserId();
            obj.ModifiedDate = DateTime.UtcNow;

            return new Response<SuggestionsVm>(_mapper.Map<SuggestionsVm>(await _SuggestionRepo.UpdateAsync(obj)));
        }

        public async Task<Response<SuggestionsVm>> GetByIdAsync(int id)
        {
            var data = await _SuggestionRepo.GetByIdAsync(id);
            if (data == null)
            {
                throw new KeyNotFoundException($"Suggestions not found by id={id}");
            }
            

            return new Response<SuggestionsVm>(_mapper.Map<SuggestionsVm>(data));
        }

        public async Task<Response<IList<SuggestionsVm>>> GetAllAsync()
        {
            var list = await _SuggestionRepo.ListAllAsync();
            list = null;
            if (list == null)
            {
                throw new KeyNotFoundException($"Suggestions not found");
            }

            return new Response<IList<SuggestionsVm>>(_mapper.Map<IList<SuggestionsVm>>(list));
        }
        public async Task<PagedResponse<IList<SuggestionsVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null)
        {

            List<Expression<Func<Suggestion, bool>>> queryFilter = new List<Expression<Func<Suggestion, bool>>>();
            List<Expression<Func<Suggestion, Object>>> includes = new List<Expression<Func<Suggestion, Object>>>();

           
            var userLoginId = base.GetLoggerUserId();
            var userType =  _UserRepo.GetByIdAsync(userLoginId).Result;

           if (userType.UserTypeId == 3)//colaborador
            {
                queryFilter.Add(x => x.UserId == userLoginId);

            }
            if (userType.UserTypeId == 1)//Proveedor
            {
                queryFilter.Add(x => x.CompanyId == userType.CompanyId);

            }

            if (filter != null || filter.Length > 0)
            {
                queryFilter.Add(x => x.SuggestionComment.Contains(filter));
            }
            includes.Add(x => x.Menu);
            includes.Add(x => x.Company);
            includes.Add(x => x.User);
            //includes.Add(x => x.TypeSuggestion);

            var list = await _SuggestionRepo.GetPagedList(pageNumber, pageSize, queryFilter, includes: includes);
            if (list == null || list.Data.Count == 0)
            {
                throw new KeyNotFoundException($"Suggestions not found");
            }

           

            IList< SuggestionsVm> listSuggestionsVm = _mapper.Map<IList<SuggestionsVm>>(list.Data);
            for (int i = 0; i < list.Data.Count; i++)
            {
                if (list.Data[i].SuggestionId == listSuggestionsVm[i].SuggestionId)
                {
                    listSuggestionsVm[i].Companyname = list.Data[i].Company.CompanyName;
                    listSuggestionsVm[i].MenuName = list.Data[i].Menu.Name;
                    listSuggestionsVm[i].MenuName = list.Data[i].Menu.Name;
                    listSuggestionsVm[i].UserName = string.Concat(list.Data[i].User.FirstName, " ", list.Data[i].User.FirstLastName);
                    //listSuggestionsVm[i].TypeSuggestion = list.Data[i].TypeSuggestion.TypeSuggestionName;

                }
                
            }


            return new PagedResponse<IList<SuggestionsVm>>(listSuggestionsVm, list.PageNumber, list.PageSize, list.TotalCount);
        }

        public async Task<PagedResponse<IList<VReportSuggestionVm>>> GetPagedListReportAsync(int pageNumber, int pageSize, string filter = null)
        {

            List<Expression<Func<VReportSuggestion, bool>>> queryFilter = new List<Expression<Func<VReportSuggestion, bool>>>();
            List<Expression<Func<VReportSuggestion, Object>>> includes = new List<Expression<Func<VReportSuggestion, Object>>>();


            var userid = base.GetLoggerUserId();
            queryFilter.Add(x => x.UserId.Equals(userid));
            if (filter != null || filter.Length > 0)
            {
                queryFilter.Add(x => x.SuggestionComment.Contains(filter));
            }
            var list = await _VReportSuggestionRepo.GetPagedList(pageNumber, pageSize, queryFilter, includes: includes);
            if (list == null || list.Data.Count == 0)
            {
                throw new KeyNotFoundException($"ReportSuggestion not found");
            }

            return new PagedResponse<IList<VReportSuggestionVm>>(_mapper.Map<IList<VReportSuggestionVm>>(list.Data), list.PageNumber, list.PageSize, list.TotalCount);
        }
    }
}
