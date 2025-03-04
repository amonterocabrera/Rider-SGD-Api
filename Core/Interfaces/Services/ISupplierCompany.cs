using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SGDPEDIDOS.Application.Interfaces.Services
{
    public interface ISupplierCompany
    {
        Task<Response<SupplierCompanyVm>> InsertAsync(SupplierCompanyDto dto);

        Task<Response<SupplierCompanyVm>> UpdateAsync(int id, SupplierCompanyDto dto);

        Task<Response<IList<SupplierCompanyViewVm>>> GetAllAsync();
        Task<Response<IList<SupplierCompanyViewVm>>> GetALLGetAllCompanyAsync();
        Task<Response<SupplierCompanyVm>> GetByIdAsync(int id);

        Task<PagedResponse<IList<SupplierCompanyVm>>> GetPagedListAsync(int pageNumber, int pageSize, int SupplierId);
    }
}
