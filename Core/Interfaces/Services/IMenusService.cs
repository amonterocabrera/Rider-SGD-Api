using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.Interfaces.Services
{
    public interface IMenusService
    {
        Task<Response<MenusVm>> InsertAsync(MenusDto dto);
        Task<Response<MenusVm>> CreateDishes(MenusDto dto);
        Task<Response<MenusVm>> UpdateDishes(MenusDto dto);
        
        Task<Response<bool>> CopyMenuAsync(CopyMenusDto dto);

        Task<Response<DtoAddRemoveImage>> AddandRemovePitucture(DtoAddRemoveImage dto);
        Task<Response<MenusVm>> UpdateAsync(int id, MenusDto dto);
        Task<Response<MenusVm>> ActiveInactive(int MenuId, bool IsActive);
        Task<Response<MenusVm>> DeleteAsync(int MenuId);

        Task<Response<IList<MenusVm>>> GetAllAsync();
        Task<Response<MenuCompany>> GetByIdAsync(int id);
        
        Task<PagedResponse<IList<MenuCompany>>> GetPagedListAsync(int pageNumber, int pageSize, int? weeklyMenusId, int SupplierId);
        Task<PagedResponse<MenuCompany>> GeActiveInactiveListAsync(int pageNumber, int pageSize, int weeklyMenuId, int weeklyDayId, bool? IsActive);
        Task<List<MenuCompany>> GeActiveInactiveListAll();
        Task<PagedResponse<MenuCompany>> GeActiveInactiveListAllPaginate(int pageNumber, int pageSize, string searchText, bool IsActive, int type);

    }
}
