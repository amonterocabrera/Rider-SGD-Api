using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.Interfaces.Services
{
    public interface IWeeklyMenusService
    {
        Task<Response<WeeklyMenusVm>> InsertAsync(WeeklyMenusDto dto);

        Task<Response<WeeklyMenusVm>> UpdateAsync(int id, WeeklyMenusDto dto);


        Task<Response<WeeklyMenusVm>> GetByIdAsync(int id);

        Task<PagedResponse<IList<WeeklyMenusVm>>> GetPagedListAsync(int pageNumber, int pageSize, int? SupplierId);

    } }
