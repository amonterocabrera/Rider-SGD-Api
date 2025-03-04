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
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Infrastructure.Services
{
    public class CompanyService : BaseService, ICompanyService
    {
        private readonly IRepositoryAsync<Company> _CompanyRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<CompanyDto> _validator;
        public CompanyService(IRepositoryAsync<Company> CompanyRepo, IMapper mapper, IValidator<CompanyDto> validator, IHttpContextAccessor context) : base(context)
        {
            _CompanyRepo = CompanyRepo;
            _mapper = mapper;
            _validator = validator;
        }

        private async Task<bool> ExitsAsync(int Id)
        {
            var result = await _CompanyRepo.Exists(x => x.CompanyId.Equals(Id));
            if (result) { return true; }
            throw new ApiException("Company not found");
        }

        public async Task<Response<CompanyVm>> InsertAsync(CompanyDto dto)
        {
            try
            {
                var valResult = _validator.Validate(dto);
                if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

                var obj = _mapper.Map<Company>(dto);

                obj.CreationDate = DateTime.UtcNow;
                obj.CreatedBy = base.GetLoggerUserId();
                obj.CompanyName = dto.CompanyName;
                obj.BrandName = dto.CompanyName;
                return new Response<CompanyVm>(_mapper.Map<CompanyVm>(await _CompanyRepo.AddAsync(obj)));

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<Response<CompanyVm>> UpdateAsync(int id, CompanyDto dto)
        {
            try
            {
                var valResult = _validator.Validate(dto);
                if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);
                await ExitsAsync(id);

                var objDb = await _CompanyRepo.WhereAsync(x => x.CompanyId.Equals(id));
                var obj = _mapper.Map<Company>(dto);

                obj.CompanyId = objDb.CompanyId;
                obj.ModifiedDate = DateTime.UtcNow;
                obj.ModifiedBy = base.GetLoggerUserId();
                obj.CompanyName = dto.CompanyName;
                obj.BrandName = dto.CompanyName;

                return new Response<CompanyVm>(_mapper.Map<CompanyVm>(await _CompanyRepo.UpdateAsync(obj)));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Response<CompanyVm>> GetByIdAsync(int id)
        {
            var data = await _CompanyRepo.GetByIdAsync(id);
            if (data == null)
            {
                throw new KeyNotFoundException($"Company not found by id={id}");
            }

            return new Response<CompanyVm>(_mapper.Map<CompanyVm>(data));
        }



        public async Task<PagedResponse<IList<CompanyVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null)
        {

            List<Expression<Func<Company, bool>>> queryFilter = new List<Expression<Func<Company, bool>>>();
            List<Expression<Func<Company, Object>>> includes = new List<Expression<Func<Company, Object>>>();

            
            var companyid = base.GetLoggerUserOrganizationId();
            queryFilter.Add(x => x.CompanyId.Equals(companyid));
            if (filter != null || filter.Length > 0)
            {
                queryFilter.Add(x => x.CompanyName.Contains(filter));
            }


            var list = await _CompanyRepo.GetPagedList(pageNumber, pageSize, queryFilter, includes: includes, orderBy: q => q.OrderByDescending(x => x.IsActive));
         

            return new PagedResponse<IList<CompanyVm>>(_mapper.Map<IList<CompanyVm>>(list.Data.OrderByDescending(d => d.IsActive).ToList()), list.PageNumber, list.PageSize, list.TotalCount);
        }

        public async Task<PagedResponse<IList<CompanyVm>>> GetAllPagedListAsync(int pageNumber, int pageSize, string filter = null, int? companyType = 0)
        {

            List<Expression<Func<Company, bool>>> queryFilter = new List<Expression<Func<Company, bool>>>();
            List<Expression<Func<Company, Object>>> includes = new List<Expression<Func<Company, Object>>>();


            var companyid = base.GetLoggerUserOrganizationId();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                queryFilter.Add(x => x.CompanyName.Contains(filter));
            }

            if (companyType > 0)
            {
                queryFilter.Add(x => x.TypeCompanyId == companyType);
            }

            var list = await _CompanyRepo.GetPagedList(pageNumber, pageSize, queryFilter, includes: includes, orderBy: q => q.OrderByDescending(x => x.IsActive==true));
        
            return new PagedResponse<IList<CompanyVm>>(_mapper.Map<IList<CompanyVm>>(list.Data.OrderByDescending(d=> d.IsActive).ToList()), list.PageNumber, list.PageSize, list.TotalCount);
        }

        public async Task<PagedResponse<IList<CompanyVm>>> GetAllListAsync(int typeCompanyId)
        {
            var companyId = base.GetLoggerUserOrganizationId();
            int pageNumber = 0;
            int pageSize = 0;
            var list = await _CompanyRepo.WhereAllAsync(d => d.TypeCompanyId == typeCompanyId && d.IsActive == true);

            if (list == null || list.Count == 0)
            {
                throw new Exception($"No companies found for TypeCompanyId: {typeCompanyId}");
            }

            var data = _mapper.Map<IList<CompanyVm>>(list);

            var pagedData = data.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var totalRecords = data.Count;

            return new PagedResponse<IList<CompanyVm>>(data.OrderByDescending(d=> d.IsActive).ToList(), pageNumber, pageSize, totalRecords);
        }

        public async Task<PagedResponse<IList<CompanyVm>>> GetAllCompaniesAdmin(int typeCompanyId)
        {
            var companyId = base.GetLoggerUserOrganizationId();
            int pageNumber = 0;
            int pageSize = 0;
            var list = await _CompanyRepo.WhereAllAsync(d => d.TypeCompanyId == typeCompanyId);

      
            var data = _mapper.Map<IList<CompanyVm>>(list);

            var pagedData = data.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var totalRecords = data.Count;

            return new PagedResponse<IList<CompanyVm>>(data.OrderByDescending(d=> d.IsActive).ToList(), pageNumber, pageSize, totalRecords);
        }




    }
}
