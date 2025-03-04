using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.Interfaces.Services
{
    public interface IFavoritesService
    {
        Task<Response<FavoritesVm>> InsertAsync(FavoritesDto dto);

        Task<Response<FavoritesVm>> DeleteAsync(FavoritesDto dto);


        Task<Response<FavoritesVm>> GetByIdAsync(int id);

        Task<PagedResponse<IList<FavoritesDetalleVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null);

    } }
