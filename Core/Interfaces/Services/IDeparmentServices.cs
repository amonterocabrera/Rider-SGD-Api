using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.Interfaces.Services
{
    public interface IDeparmentServices
    {
        Task<Response<DeparmentVm>> InsertAsync(DeparmentDto dto);

        Task<Response<DeparmentVm>> UpdateAsync(int id, DeparmentDto dto);


        Task<Response<DeparmentVm>> GetByIdAsync(int id);

        Task<Response<IList<DeparmentVm>>> GetAllAsync();

        Task<PagedResponse<IList<DeparmentVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null);

    } }
