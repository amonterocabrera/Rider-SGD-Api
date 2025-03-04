using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.Interfaces.Services
{
    public interface IWeeklyDaysService
    {
        Task<Response<WeeklyDaysVm>> InsertAsync(WeeklyDaysDto dto);

        Task<Response<WeeklyDaysVm>> UpdateAsync(int id, WeeklyDaysDto dto);


        Task<Response<WeeklyDaysVm>> GetByIdAsync(int id);

        Task<PagedResponse<IList<WeeklyDaysVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null);

    } }
