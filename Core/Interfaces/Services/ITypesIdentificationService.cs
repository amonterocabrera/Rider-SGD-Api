using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.Interfaces.Services
{
    public interface ITypesIdentificationService
    {
        Task<Response<TypesIdentificationsVm>> InsertAsync(TypesIdentificationsDto dto);

        Task<Response<TypesIdentificationsVm>> UpdateAsync(int id, TypesIdentificationsDto dto);


        Task<Response<TypesIdentificationsVm>> GetByIdAsync(int id);

        Task<Response<IList<TypesIdentificationsVm>>> GetAllAsync();

        Task<PagedResponse<IList<TypesIdentificationsVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null);

    } }
