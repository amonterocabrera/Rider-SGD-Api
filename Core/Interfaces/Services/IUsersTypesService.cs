using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.Interfaces.Services
{
    public interface IUsersTypesService
    {
        Task<Response<UsersTypesVm>> InsertAsync(UsersTypesDto dto);

        Task<Response<UsersTypesVm>> UpdateAsync(int id, UsersTypesDto dto);


        Task<Response<UsersTypesVm>> GetByIdAsync(int id);

        Task<Response<IList<UsersTypesVm>>> GetAllAsync();

        Task<PagedResponse<IList<UsersTypesVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null);

    } }
