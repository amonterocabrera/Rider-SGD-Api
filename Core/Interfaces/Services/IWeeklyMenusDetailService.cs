using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.Wrappers;
using SGDPEDIDOS.Domain.Entities;
using SGDPEDIDOS.Domain.Entities.StoreProcedure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.Interfaces.Services
{
    public interface IWeeklyMenusDetailService
    {
        Task<Response<WeeklyMenusDetailVm>> InsertAsync(WeeklyMenusDetailDto dto);

        Task<Response<WeeklyMenusDetailVm>> UpdateAsync(int id, WeeklyMenusDetailDto dto);

        Task<List<dynamic>> sp_week_menuAsync(int? weekly_menus_id);
        Task<List<dynamic>> getWeekAllBeforeMenus(int weekly_menus_id);
        

        Task<Response<WeeklyMenusDetailVm>> GetByIdAsync(int id);

        Task<PagedResponse<IList<WeeklyMenusDetailVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null);

        Task<int> sp_insert_delete_week_menu_supplierAsync(int? weekly_menus_id, int? weekly_day, int? menu_id, bool? is_active);

        Task<dynamic> sp_view_week_menu_supplierAsync(int? weekly_menus_id, int? weekly_day);

    } 
}
