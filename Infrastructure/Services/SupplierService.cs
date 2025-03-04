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
using SGDPEDIDOS.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DSCALIDAD.Infrastructure.Services
{
    public class SupplierService : BaseService, ISupplierServices
    {
        private readonly IRepositoryAsync<Company> _CompanyRepo;
        private readonly IRepositoryAsync<VReportSupplier> _VReportSupplierRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<CompanyDto> _validator;
        public SupplierService(IRepositoryAsync<Company> CompanyRepo, IRepositoryAsync<VReportSupplier> VReportSupplierRepo, IMapper mapper, IValidator<CompanyDto> validator, IHttpContextAccessor context) : base(context)
        {
            _CompanyRepo = CompanyRepo;
            _VReportSupplierRepo = VReportSupplierRepo; 
            _mapper = mapper;
            _validator = validator;
        }

        private async Task<bool> ExitsAsync(int Id)
        {
            var result = await _CompanyRepo.Exists(x => x.CompanyId.Equals(Id));
            if (result) { return true; }
            throw new ApiException("Supplier not found");
        }

        public async Task<Response<SupplierVm>> InsertAsync(CompanyDto dto)
        {
            dto.TypeCompanyId = 1;
            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            var obj = _mapper.Map<Company>(dto);

            obj.CreationDate = DateTime.UtcNow;
            obj.CreatedBy = base.GetLoggerUserId();


            return new Response<SupplierVm>(_mapper.Map<SupplierVm>(await _CompanyRepo.AddAsync(obj)));

        }

        public async Task<Response<SupplierVm>> UpdateAsync(int id, CompanyDto dto)
        {
            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);
            dto.TypeCompanyId = 1;
            await ExitsAsync(id);

            var objDb = await _CompanyRepo.WhereAsync(x => x.CompanyId.Equals(id));
            var obj = _mapper.Map<Company>(dto);

            obj.CompanyId = objDb.CompanyId;
            obj.ModifiedDate = DateTime.UtcNow; 
            obj.ModifiedBy = base.GetLoggerUserId();


            return new Response<SupplierVm>(_mapper.Map<SupplierVm>(await _CompanyRepo.UpdateAsync(obj)));
        }

        public async Task<Response<SupplierVm>> GetByIdAsync(int id)
        {
            
            var data = await _CompanyRepo.GetByIdAsync(id);
            if (data == null)
            {
                throw new KeyNotFoundException($"Supplier not found by id={id}");
            }

            return new Response<SupplierVm>(_mapper.Map<SupplierVm>(data));
        }



        public async Task<PagedResponse<IList<SupplierVm>>> GetPagedListAsync(int pageNumber, int pageSize, int? SupplierId)
        {

            List<Expression<Func<Company, bool>>> queryFilter = new List<Expression<Func<Company, bool>>>();
            List<Expression<Func<Company, Object>>> includes = new List<Expression<Func<Company, Object>>>();

            
            var companyid = base.GetLoggerUserOrganizationId();
            queryFilter.Add(x => x.CompanyId.Equals(companyid));
            if (SupplierId != null || SupplierId > 0)
            {
                queryFilter.Add(x => x.CompanyId== SupplierId);
            }

            var list = await _CompanyRepo.GetPagedList(pageNumber, pageSize, queryFilter, includes: includes);
            if (list == null || list.Data.Count == 0)
            {
                throw new KeyNotFoundException($"Supplier not found");
            }

            return new PagedResponse<IList<SupplierVm>>(_mapper.Map<IList<SupplierVm>>(list.Data), list.PageNumber, list.PageSize, list.TotalCount);
        }

        public async Task<PagedResponse<IList<VReportSupplierVm>>> GetPagedListReportAsync(int pageNumber, int pageSize, int? SupplierId)
        {

            List<Expression<Func<VReportSupplier, bool>>> queryFilter = new List<Expression<Func<VReportSupplier, bool>>>();
            List<Expression<Func<VReportSupplier, Object>>> includes = new List<Expression<Func<VReportSupplier, Object>>>();


            var companyid = base.GetLoggerUserOrganizationId();
            queryFilter.Add(x => x.SupplierId.Equals(companyid));
            if (SupplierId != null || SupplierId > 0)
            {
                queryFilter.Add(x => x.SupplierId == SupplierId);
            }

            var list = await _VReportSupplierRepo.GetPagedList(pageNumber, pageSize, queryFilter, includes: includes);
            if (list == null || list.Data.Count == 0)
            {
                throw new KeyNotFoundException($"ReportSupplier not found");
            }

            return new PagedResponse<IList<VReportSupplierVm>>(_mapper.Map<IList<VReportSupplierVm>>(list.Data), list.PageNumber, list.PageSize, list.TotalCount);
        }


    }
}
