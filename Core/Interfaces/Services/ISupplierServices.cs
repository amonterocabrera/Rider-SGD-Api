using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.Wrappers;
using SGDPEDIDOS.Domain.Entities.View;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.Interfaces.Services
{
    public interface ISupplierServices
    {
        Task<Response<SupplierVm>> InsertAsync(CompanyDto dto);

        Task<Response<SupplierVm>> UpdateAsync(int id, CompanyDto dto);


        Task<Response<SupplierVm>> GetByIdAsync(int id);

        Task<PagedResponse<IList<SupplierVm>>> GetPagedListAsync(int pageNumber, int pageSize, int? SupplierId);
        Task<PagedResponse<IList<VReportSupplierVm>>> GetPagedListReportAsync(int pageNumber, int pageSize, int? SupplierId);

    } }
