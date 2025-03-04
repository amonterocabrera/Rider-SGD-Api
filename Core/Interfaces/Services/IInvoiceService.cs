using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGDPEDIDOS.Api.Models;
using SGDPEDIDOS.Domain.Entities.View;

namespace SGDPEDIDOS.Application.Interfaces.Services
{
    public interface IInvoiceService
    {
        Task<Response<List<InvoiceVm>>> InsertAsync(List<InvoiceDto> dto);

        Task<Response<InvoiceVm>> UpdateAsync(int id, InvoiceDto dto);
        Task<Response<InvoiceVm>> ChangeStatus(int id, InvoiceStatusDto dto);
        Task<Response<InvoiceVm>> GetByIdAsync(int id);
        Task<PagedResponse<IList<InvoiceVm>>> GetPagedListAsync(int pageNumber, int pageSize, int orderStatusId);
        Task<PagedResponse<IList<VReportVm>>> GetPagedViewListAsync(int pageNumber, int pageSize, int Day, int SupplierId, string filter = null);
        Task<PagedResponse<IList<VReportVm>>> GetPagedViewCompanyListAsync(int pageNumber, int pageSize, string Day, int SupplierId, int statusId, string filter = null);
        Task<PagedResponse<IList<VReportSupplierVM>>> GetPagedViewSuppliedDetailsListAsync(int pageNumber, int pageSize, int supplierId, int selectedMonth, int selectedYear);
        Task<PagedResponse<IList<VReportSupplierGroupVM>>> GetPagedViewGroupListAsync(int pageNumber, int pageSize, int supplierId, int selectedMonth, int selectedYear);
       // Task<PagedResponse<IList<VReportSupplierVM>>> GetPagedViewSuppliedDetailsListSupplier(int pageNumber, int pageSize, string Day, int EmpresaId, string filter = null);
        Task<PagedResponse<IList<VReportSupplierGroupVM>>> GetPagedViewGroupListSupplier(int pageNumber, int pageSize, string Day, int EmpresaId, string filter = null);
       // Task<PagedResponse<IList<VReportUserConsumedClient>>> GetReportUserConsumedClient(int pageNumber, int pageSize, string Day,string filter = null);       

    }
}
