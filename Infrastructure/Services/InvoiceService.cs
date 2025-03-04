using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SGDPEDIDOS.Api.Models;
using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.Exceptions;
using SGDPEDIDOS.Application.Interfaces.Repositories;
using SGDPEDIDOS.Application.Interfaces.Services;
using SGDPEDIDOS.Application.Wrappers;
using SGDPEDIDOS.Domain.Entities;
using SGDPEDIDOS.Domain.Entities.View;
using SGDPEDIDOS.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SGDPEDIDOS.Infrastructure.Services
{
    public class InvoiceService : BaseService, IInvoiceService
    {
        private readonly IRepositoryAsync<Invoice> _Repo;
        private readonly IRepositoryAsync<VReport> _VReportRepo;
        private readonly IRepositoryAsync<VReportSupplier> _VReportSupplier;
        private readonly IRepositoryAsync<VReportSupplierGroup> _VReportSupplierGroup;

        private readonly IRepositoryAsync<InvoicesDetail> _RepoDetail;
        private readonly IMapper _mapper;
        private readonly IValidator<InvoiceDto> _validator;
        private readonly PrincipalContext _contex;
        public InvoiceService(IRepositoryAsync<Invoice> repo, PrincipalContext contex, IRepositoryAsync<InvoicesDetail> RepoDetail,
            IRepositoryAsync<VReport> VReportRepo, IRepositoryAsync<VReportSupplier> VReportSupplier, IRepositoryAsync<VReportSupplierGroup> VReportSupplierGroup, IMapper mapper, IValidator<InvoiceDto> validator, IHttpContextAccessor context) : base(context)
        {
            _Repo = repo;
            _VReportRepo = VReportRepo;
            _VReportSupplierGroup = VReportSupplierGroup;
            _VReportSupplier = VReportSupplier;
            _RepoDetail = RepoDetail;
            _mapper = mapper;
            _validator = validator;
            _contex = contex;
        }

        private async Task<bool> ExitsAsync(int Id)
        {
            var result = await _Repo.Exists(x => x.InvoiceId.Equals(Id));
            if (result) { return true; }
            throw new ApiException("Invoice not found");
        }
        Task<Response<IList<InvoiceVm>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

      public  async Task<Response<InvoiceVm>> GetByIdAsync(int id)
        {
           
            var invoiceVm = await _Repo.GetByIdAsync(id);
            
            invoiceVm.InvoicesDetails = await _RepoDetail.WhereAllAsync(d=> d.InvoiceId== id && d.DeletedDate == null);
            if (invoiceVm == null )
            {
                throw new KeyNotFoundException($"Invoice not found");
            }

            return new Response<InvoiceVm>(_mapper.Map<InvoiceVm>(invoiceVm));
        }

     public  async Task<PagedResponse<IList<InvoiceVm>>> GetPagedListAsync(int pageNumber, int pageSize, int orderStatusId)
        {
            List<Expression<Func<Invoice, bool>>> queryFilter = new List<Expression<Func<Invoice, bool>>>();
            List<Expression<Func<Invoice, Object>>> includes = new List<Expression<Func<Invoice, Object>>>();
            includes.Add(x => x.InvoicesDetails);
            int CompanyId = base.GetLoggerUserOrganizationId();
            if(orderStatusId > 0) {
            queryFilter.Add(x => x.SupplierId == CompanyId &&  x.DeletedDate == null && x.OrderStatusId == orderStatusId);
            } else
            {
                queryFilter.Add(x => x.SupplierId == CompanyId && x.DeletedDate == null);
            }

            var list = await _Repo.GetPagedList(pageNumber, pageSize, queryFilter, includes: includes);
            if (list == null || list.Data.Count == 0)
            {
                throw new KeyNotFoundException($"Invoice not found");
            }

            return new PagedResponse<IList<InvoiceVm>>(_mapper.Map<IList<InvoiceVm>>(list.Data), list.PageNumber, list.PageSize, list.TotalCount);
        }



        public async Task<Response<List<InvoiceVm>>> InsertAsync(List<InvoiceDto> dto)
        {
            var invoiceVms = new List<InvoiceVm>();
            List<Invoice> InvoiceList = new List<Invoice>();

            int userId = base.GetLoggerUserId();

            using (var transaction = await _contex.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var item in dto)
                    {
                        var valResult = _validator.Validate(item);
                        if (!valResult.IsValid)
                            throw new ApiValidationException(valResult.Errors);

                        Invoice obj = new Invoice
                        {
                            InvoicesDetails = new List<InvoicesDetail>(),
                            UserId = userId,
                            InvoiceDate = DateTime.UtcNow,
                            CreatedBy = userId,
                            CreationDate = DateTime.UtcNow,
                            CompanyId = item.CompanyId,
                            SupplierId = item.SupplierId,
                            TotalAmount = item.TotalAmount,
                            OrderStatusId = item.OrderStatusId,
                            PayrollDeduction = item.PayrollDeduction
                        };
                       

                        foreach (var invoice in item.InvoicesDetails)
                        {
                            InvoicesDetail invoicesDetail = new InvoicesDetail
                            {
                                MenuId = invoice.MenuId,
                                ProductName = invoice.ProductName,
                                Price = invoice.Price,
                                Quantity = invoice.Quantity,
                                Details = invoice.Details,
                                CreatedBy = userId,
                                CreationDate = DateTime.UtcNow
                            };

                            obj.InvoicesDetails.Add(invoicesDetail);
                        }
                           
                        invoiceVms.Add(_mapper.Map<InvoiceVm>(obj));
                        InvoiceList.Add(obj);
                    }

                    var inserInvice = _Repo.AddRangeAsync(InvoiceList);
                    await _contex.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    // Log the exception using your preferred logging framework
                    // _logger.LogError(ex, "An error occurred while processing the invoice");

                    throw new ApplicationException("An error occurred while processing the invoice", ex);
                }
            }

            return new Response<List<InvoiceVm>>(invoiceVms);
        }


        public async Task<Response<InvoiceVm>> UpdateAsync(int id, InvoiceDto dto)
        {
            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            await ExitsAsync(id);
            var objDb = await _Repo.WhereAsync(x => x.InvoiceId.Equals(id));
            var obj = _mapper.Map<Invoice>(dto);            
            obj.CreatedBy = objDb.CreatedBy;
            obj.CreationDate = objDb.CreationDate;
            obj.ModifiedBy = base.GetLoggerUserId();
            obj.ModifiedDate = DateTime.Now;           

            return new Response<InvoiceVm>(_mapper.Map<InvoiceVm>(await _Repo.UpdateAsync(obj)));
        }
        public async Task<Response<InvoiceVm>> ChangeStatus(int id, InvoiceStatusDto dto)    
        {         
           
            var objDb = await _Repo.WhereAsync(x => x.InvoiceId.Equals(dto.InvoiceId));
            objDb.ModifiedBy = base.GetLoggerUserId();
            objDb.ModifiedDate = DateTime.Now;
            objDb.OrderStatusId = dto.OrderStatusId;
            return new Response<InvoiceVm>(_mapper.Map<InvoiceVm>(await _Repo.UpdateAsync(objDb)));
        }

        public async Task<PagedResponse<IList<VReportVm>>> GetPagedViewListAsync(int pageNumber,  int pageSize, int Day,int SupplierId, string filter = null)
        {
            var userId = base.GetLoggerUserId();

            List<Expression<Func<VReport, bool>>> Queryfilter = new List<Expression<Func<VReport, bool>>>();
            Func<IQueryable<VReport>, IOrderedQueryable<VReport>> OrderByFilter = x => x.OrderByDescending(x => x.InvoiceDate);

           
            Queryfilter.Add(x => x.UserId.Equals(userId));
          
            if (Day > 0)
            {
                Queryfilter.Add(x => x.DayCurrent <= Day);
            }
            
            if (SupplierId > 0) {

                Queryfilter.Add(x => x.SupplierId.Equals(SupplierId));

            }
            
            if (filter != null && filter.Length > 0)
            {
                Queryfilter.Add(x => x.ProductName.Contains(filter) || x.SupplierName.Contains(filter));
            }

        
            var list = await _VReportRepo.GetPagedList(pageNumber, pageSize, Queryfilter, OrderByFilter);
            if (list == null || list.Data.Count <= 0)
            {
                throw new KeyNotFoundException("Invoice not found");
            }

            return new PagedResponse<IList<VReportVm>>(_mapper.Map<IList<VReportVm>>(list.Data), list.PageNumber, list.PageSize, list.TotalCount);
        }

        public async Task<PagedResponse<IList<VReportVm>>> GetPagedViewCompanyListAsync(int pageNumber, int pageSize, string Day, int SupplierId, int statusId, string filter = null)
        {
            var OrganizationId = base.GetLoggerUserOrganizationId();

            List<Expression<Func<VReport, bool>>> Queryfilter = new List<Expression<Func<VReport, bool>>>();
            Func<IQueryable<VReport>, IOrderedQueryable<VReport>> OrderByFilter = x => x.OrderByDescending(x => x.InvoiceDate);


            Queryfilter.Add(x => x.SupplierId.Equals(OrganizationId));

            if (string.IsNullOrEmpty(Day))
            {               
                DateTime parsedDay = DateTime.UtcNow;
                Queryfilter.Add(x => x.InvoiceDate.Date == parsedDay.Date);
            }
            else
            {
                
                DateTime parsedDay = DateTime.ParseExact(Day, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                Queryfilter.Add(x => x.InvoiceDate.Date == parsedDay.Date);
            }


            if (SupplierId > 0)
            {

                Queryfilter.Add(x => x.CompanyId.Equals(SupplierId));

            }
            if (statusId > 0)
            {

                Queryfilter.Add(x => x.OrderStatusId.Equals(statusId));

            }

            if (filter != null && filter.Length > 0)
            {
                Queryfilter.Add(x => x.ProductName.Contains(filter) || x.CompanyName.Contains(filter));
            }


            var list = await _VReportRepo.GetPagedList(pageNumber, pageSize, Queryfilter, OrderByFilter);
            if (list == null || list.Data.Count <= 0)
            {
                throw new KeyNotFoundException("Invoice not found");
            }

            return new PagedResponse<IList<VReportVm>>(_mapper.Map<IList<VReportVm>>(list.Data), list.PageNumber, list.PageSize, list.TotalCount);
        }

        public async Task<PagedResponse<IList<VReportSupplierVM>>> GetPagedViewSuppliedDetailsListAsync(int pageNumber, int pageSize, int supplierId, int selectedMonth, int selectedYear)
        {
            var OrganizationId = base.GetLoggerUserOrganizationId();
            List<VReportSupplierVM> vReportSupplierVMs = new List<VReportSupplierVM>();

            List<Expression<Func<VReportSupplier, bool>>> Queryfilter = new List<Expression<Func<VReportSupplier, bool>>>();
            Func<IQueryable<VReportSupplier>, IOrderedQueryable<VReportSupplier>> OrderByFilter = x => x.OrderByDescending(x => x.InvoiceDate);

         

            Queryfilter.Add(x => x.ClientId == supplierId);
            Queryfilter.Add(x => x.InvoiceDate.Year == selectedYear && x.InvoiceDate.Month == selectedMonth);


            var list = await _VReportSupplier.GetPagedList(pageNumber, pageSize, Queryfilter, OrderByFilter);
            if (list == null || list.Data.Count <= 0)
            {
                throw new KeyNotFoundException("Invoice not found");
            }

            vReportSupplierVMs = _mapper.Map<List<VReportSupplierVM>>(list.Data.OrderByDescending(d=>d.InvoiceDate));
            return new PagedResponse<IList<VReportSupplierVM>>(vReportSupplierVMs, list.PageNumber, list.PageSize, list.TotalCount);
        }

        public async Task<PagedResponse<IList<VReportSupplierGroupVM>>> GetPagedViewGroupListAsync(int pageNumber, int pageSize, int supplierId, int selectedMonth, int selectedYear)
        {
            var OrganizationId = base.GetLoggerUserOrganizationId();
            List<VReportSupplierGroupVM> vReportSupplierGroupVMs = new List<VReportSupplierGroupVM>();
            List<Expression<Func<VReportSupplierGroup, bool>>> Queryfilter = new List<Expression<Func<VReportSupplierGroup, bool>>>();
            Func<IQueryable<VReportSupplierGroup>, IOrderedQueryable<VReportSupplierGroup>> OrderByFilter = x => x.OrderByDescending(x => x.ClientEmpresa);

            
            Queryfilter.Add(x => x.SupplierId.Equals(OrganizationId));
            Queryfilter.Add(x => x.YearInvoice== selectedYear && x.MonthInvoice == selectedMonth);

         
            if (supplierId > 0)
            {
                Queryfilter.Add(x => x.ClientId == supplierId);
            }

            var list = await _VReportSupplierGroup.GetPagedList(pageNumber, pageSize, Queryfilter, OrderByFilter);
          

            vReportSupplierGroupVMs = _mapper.Map<List<VReportSupplierGroupVM>>(list.Data);
            return new PagedResponse<IList<VReportSupplierGroupVM>>(vReportSupplierGroupVMs, list.PageNumber, list.PageSize, list.TotalCount);
        }

        
        public async Task<PagedResponse<IList<VReportSupplierGroupVM>>> GetPagedViewGroupListSupplier(int pageNumber, int pageSize, string Day, int EmpresaId, string filter = null)
        {
            var OrganizationId = base.GetLoggerUserOrganizationId();
            List<VReportSupplierGroupVM> vReportSupplierGroupVMs = new List<VReportSupplierGroupVM>();
            List<Expression<Func<VReportSupplierGroup, bool>>> Queryfilter = new List<Expression<Func<VReportSupplierGroup, bool>>>();
            Func<IQueryable<VReportSupplierGroup>, IOrderedQueryable<VReportSupplierGroup>> OrderByFilter = x => x.OrderByDescending(x => x.ClientEmpresa);

            DateTime parsedDay = string.IsNullOrEmpty(Day) == true ? DateTime.UtcNow : DateTime.ParseExact(Day, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            Queryfilter.Add(x => x.ClientId.Equals(OrganizationId));
            Queryfilter.Add(x => x.YearInvoice == parsedDay.Year && x.MonthInvoice == parsedDay.Month);

            if (filter != null && filter.Length > 0)
            {
                Queryfilter.Add(x => string.Concat(x.ClientEmpresa).Contains(filter));
            }
            if (EmpresaId != null && EmpresaId > 0)
            {
                Queryfilter.Add(x => x.SupplierId == EmpresaId);
            }

            var list = await _VReportSupplierGroup.GetPagedList(pageNumber, pageSize, Queryfilter, OrderByFilter);
            if (list == null || list.Data.Count <= 0)
            {
                throw new KeyNotFoundException("Invoice not found");
            }

            vReportSupplierGroupVMs = _mapper.Map<List<VReportSupplierGroupVM>>(list.Data);
            return new PagedResponse<IList<VReportSupplierGroupVM>>(vReportSupplierGroupVMs, list.PageNumber, list.PageSize, list.TotalCount);
        }

      
    }
}
