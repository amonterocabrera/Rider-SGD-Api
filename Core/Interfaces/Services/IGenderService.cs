using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.Interfaces.Services
{
    public interface IGenderService
    {
        Task<Response<GenderVm>> InsertAsync(GenderDto dto);

        Task<Response<GenderVm>> UpdateAsync(int id, GenderDto dto);


        Task<Response<GenderVm>> GetByIdAsync(int id);

        Task<Response<IList<GenderVm>>> GetAllAsync();

        Task<PagedResponse<IList<GenderVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null);

    } }
