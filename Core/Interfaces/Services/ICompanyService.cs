using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.Wrappers;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.Interfaces.Services
{
    public interface ICompanyService
    {
        Task<Response<CompanyVm>> InsertAsync(CompanyDto dto);

        Task<Response<CompanyVm>> UpdateAsync(int id, CompanyDto dto);


        Task<Response<CompanyVm>> GetByIdAsync(int id);

        Task<PagedResponse<IList<CompanyVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null);
        Task<PagedResponse<IList<CompanyVm>>> GetAllPagedListAsync(int pageNumber, int pageSize, string filter = null, int? companyType = 0);
        Task<PagedResponse<IList<CompanyVm>>> GetAllListAsync(int typeCompanyId);
        Task<PagedResponse<IList<CompanyVm>>> GetAllCompaniesAdmin(int typeCompanyId);



    } }
