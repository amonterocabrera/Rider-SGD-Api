using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.Interfaces.Services
{
    public interface IUsersService
    {
        Task<Response<UsersVm>> RegisterAsync(UsersDto dto);
        Task<Response<UsersVm>> InsertAsync(UsersDto dto);
        Task<Response<List<UsersVm>>> InsertListAsync(List<UsersDtoList> dto);

        Task<Response<List<UsersVm>>> UpdateBalanceListAsync(List<CreditAvailableDtoList> dto);
        Task<Response<UsersVm>> InsertImagenAsync(UsersImagenDto dto);
        Task<Response<UsersVm>> GetByIdAsync(int id);
        Task<Response<UsersVm>> UpdateAsync(int id, UsersDto dto);
        Task<Response<UsersVm>> UpdateSubsidyamount(int id, UpdateSubsidyAmountDto dto);
        Task<Response<UsersVm>> UpdatePasswordAsync(int id, UpdatePasswordUsersDto dto);
        Task<PagedResponse<IList<UsersVm>>> GetPagedListAsync(int pageNumber, int pageSize,string filter = null);
        Task<PagedResponse<IList<UsersVm>>> GetUserListAdmin(int pageNumber, int pageSize, int companyId, int userTypeId, string filter = null);

    } }
