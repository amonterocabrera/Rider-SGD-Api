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
using SGDPEDIDOS.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DSCALIDAD.Infrastructure.Services
{
    public class TypeSuggestionsService : BaseService, ITypeSuggestionsService
    {
        private readonly IRepositoryAsync<TypeSuggestion> _TypeSuggestionRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<TypeSuggestionsDto> _validator;
        public TypeSuggestionsService(IRepositoryAsync<TypeSuggestion> TypeSuggestionRepo, IMapper mapper, IValidator<TypeSuggestionsDto> validator, IHttpContextAccessor context) : base(context)
        {
            _TypeSuggestionRepo = TypeSuggestionRepo;
            _mapper = mapper;
            _validator = validator;
        }

        private async Task<bool> ExitsAsync(int Id)
        {
            var result = await _TypeSuggestionRepo.Exists(x => x.TypeSuggestionId.Equals(Id));
            if (result) { return true; }
            throw new ApiException("TypeSuggestion not found");
        }

        public async Task<Response<TypeSuggestionsVm>> InsertAsync(TypeSuggestionsDto dto)
        {

            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            var obj = _mapper.Map<TypeSuggestion>(dto);


            return new Response<TypeSuggestionsVm>(_mapper.Map<TypeSuggestionsVm>(await _TypeSuggestionRepo.AddAsync(obj)));

        }

        public async Task<Response<TypeSuggestionsVm>> UpdateAsync(int id, TypeSuggestionsDto dto)
        {
            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            await ExitsAsync(id);

            var objDb = await _TypeSuggestionRepo.WhereAsync(x => x.TypeSuggestionId.Equals(id));
            var obj = _mapper.Map<TypeSuggestion>(dto);

            obj.TypeSuggestionId = objDb.TypeSuggestionId;


            return new Response<TypeSuggestionsVm>(_mapper.Map<TypeSuggestionsVm>(await _TypeSuggestionRepo.UpdateAsync(obj)));
        }

        public async Task<Response<TypeSuggestionsVm>> GetByIdAsync(int id)
        {
            var data = await _TypeSuggestionRepo.GetByIdAsync(id);
            if (data == null)
            {
                throw new KeyNotFoundException($"TypeSuggestion not found by id={id}");
            }

            return new Response<TypeSuggestionsVm>(_mapper.Map<TypeSuggestionsVm>(data));
        }
        public async Task<Response<IList<TypeSuggestionsVm>>> GetAllAsync()
        {
            var list = await _TypeSuggestionRepo.ListAllAsync();
            if (list == null)
            {
                throw new KeyNotFoundException($"TypeSuggestion not found");
            }

            return new Response<IList<TypeSuggestionsVm>>(_mapper.Map<IList<TypeSuggestionsVm>>(list));
        }



        public async Task<PagedResponse<IList<TypeSuggestionsVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null)
        {

            List<Expression<Func<TypeSuggestion, bool>>> queryFilter = new List<Expression<Func<TypeSuggestion, bool>>>();
            List<Expression<Func<TypeSuggestion, Object>>> includes = new List<Expression<Func<TypeSuggestion, Object>>>();




            //if (filter != null || filter.Length > 0)
            //{
            //    queryFilter.Add(x => x.Id();
            //}

            var list = await _TypeSuggestionRepo.GetPagedList(pageNumber, pageSize, queryFilter, includes: includes);
            if (list == null || list.Data.Count == 0)
            {
                throw new KeyNotFoundException($"TypeSuggestion not found");
            }

            return new PagedResponse<IList<TypeSuggestionsVm>>(_mapper.Map<IList<TypeSuggestionsVm>>(list.Data), list.PageNumber, list.PageSize, list.TotalCount);
        }


    }
}
