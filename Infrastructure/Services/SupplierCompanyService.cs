using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.Exceptions;
using SGDPEDIDOS.Application.Interfaces.Repositories;
using SGDPEDIDOS.Application.Interfaces.Services;
using SGDPEDIDOS.Application.Wrappers;
using SGDPEDIDOS.Domain.Entities;
using SGDPEDIDOS.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Infrastructure.Services
{
    public class SupplierCompanyService : BaseService, ISupplierCompany
    {
        private readonly IRepositoryAsync<SupplierCompany> _SupplierCompanyRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<SupplierCompanyDto> _validator;
        private readonly PrincipalContext _principalRepo;    

        public SupplierCompanyService(IRepositoryAsync<SupplierCompany> SupplierCompanyRepo, PrincipalContext principalRepo, IMapper mapper, IValidator<SupplierCompanyDto> validator, IHttpContextAccessor context) : base(context)
        {
            _SupplierCompanyRepo = SupplierCompanyRepo;
            _principalRepo = principalRepo; 
            _mapper = mapper;

        }
        private async Task<bool> ExitsAsync(int Id)
        {
            var result = await _SupplierCompanyRepo.Exists(x => x.SupplierCompanyId.Equals(Id));
            if (result) { return true; }
            throw new ApiException("SupplierCompany not found");
        }

        public async Task<Response<SupplierCompanyVm>> InsertAsync(SupplierCompanyDto dto)
        {

            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            var obj = _mapper.Map<SupplierCompany>(dto);

            obj.IsActive = true;


            return new Response<SupplierCompanyVm>(_mapper.Map<SupplierCompanyVm>(await _SupplierCompanyRepo.AddAsync(obj)));

        }

        public async Task<Response<SupplierCompanyVm>> UpdateAsync(int id, SupplierCompanyDto dto)
        {
            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            await ExitsAsync(id);

            var objDb = await _SupplierCompanyRepo.WhereAsync(x => x.SupplierCompanyId.Equals(id));
            var obj = _mapper.Map<SupplierCompany>(dto);

            obj.IsActive = false;


            return new Response<SupplierCompanyVm>(_mapper.Map<SupplierCompanyVm>(await _SupplierCompanyRepo.UpdateAsync(obj)));
        }

        public async Task<Response<SupplierCompanyVm>> GetByIdAsync(int id)
        {
            var data = await _SupplierCompanyRepo.GetByIdAsync(id);
            if (data == null)
            {
                throw new KeyNotFoundException($"SupplierCompany not found by id={id}");
            }

            return new Response<SupplierCompanyVm>(_mapper.Map<SupplierCompanyVm>(data));
        }

        public async Task<Response<IList<SupplierCompanyViewVm>>> GetAllAsync()
        {
            int companyId = base.GetLoggerUserOrganizationId();
       

            var viewModelList = await (from supplierCompany in _principalRepo.SupplierCompany
                                       join company in _principalRepo.Companies on supplierCompany.CompanyId equals company.CompanyId
                                       where supplierCompany.CompanyId == companyId && supplierCompany.IsActive == true
                                       select new SupplierCompanyViewVm
                                       {
                                           SupplierId = supplierCompany.SupplierId,
                                           SupplierName = company.CompanyName
                                       }).Distinct().ToListAsync();

            if (!viewModelList.Any())
            {
                throw new KeyNotFoundException("No matching SupplierCompany found");
            }

            return new Response<IList<SupplierCompanyViewVm>>(viewModelList);
        }

        public async Task<Response<IList<SupplierCompanyViewVm>>> GetALLGetAllCompanyAsync()
        {
            int SupplierId = base.GetLoggerUserOrganizationId();


            var viewModelList = await (from supplierCompany in _principalRepo.SupplierCompany
                                       join company in _principalRepo.Companies on supplierCompany.CompanyId equals company.CompanyId
                                       where supplierCompany.SupplierId == SupplierId && supplierCompany.IsActive == true
                                       select new SupplierCompanyViewVm
                                       {
                                           SupplierId = company.CompanyId,
                                           SupplierName = company.CompanyName
                                       }).Distinct().ToListAsync();

            if (!viewModelList.Any())
            {
                throw new KeyNotFoundException("No matching SupplierCompany found");
            }

            return new Response<IList<SupplierCompanyViewVm>>(viewModelList);
        }

        


        public async Task<PagedResponse<IList<SupplierCompanyVm>>> GetPagedListAsync(int pageNumber, int pageSize, int SupplierId)
        {

            List<Expression<Func<SupplierCompany, bool>>> queryFilter = new List<Expression<Func<SupplierCompany, bool>>>();
            List<Expression<Func<SupplierCompany, Object>>> includes = new List<Expression<Func<SupplierCompany, Object>>>();


            var SupplierCompanyid = base.GetLoggerUserOrganizationId();
            queryFilter.Add(x => x.SupplierCompanyId.Equals(SupplierCompanyid));

            if (SupplierId != null || SupplierId > 0)
            {
                queryFilter.Add(x => x.SupplierCompanyId == SupplierId);
            }

            var list = await _SupplierCompanyRepo.GetPagedList(pageNumber, pageSize, queryFilter, includes: includes);
            if (list == null || list.Data.Count == 0)
            {
                throw new KeyNotFoundException($"SupplierCompany not found");
            }

            return new PagedResponse<IList<SupplierCompanyVm>>(_mapper.Map<IList<SupplierCompanyVm>>(list.Data), list.PageNumber, list.PageSize, list.TotalCount);
        }
    }
}
