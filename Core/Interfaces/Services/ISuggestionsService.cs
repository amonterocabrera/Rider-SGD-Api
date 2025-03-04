using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.Wrappers;
using SGDPEDIDOS.Domain.Entities.View;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.Interfaces.Services
{
    public interface ISuggestionsService
    {
        Task<Response<SuggestionsVm>> InsertAsync(SuggestionsDto dto);

        Task<Response<SuggestionsVm>> UpdateAsync(int id, SuggestionsDto dto);

        Task<Response<IList<SuggestionsVm>>> GetAllAsync();
        Task<Response<SuggestionsVm>> GetByIdAsync(int id);

        Task<PagedResponse<IList<SuggestionsVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null);
        Task<PagedResponse<IList<VReportSuggestionVm>>> GetPagedListReportAsync(int pageNumber, int pageSize, string filter = null);

    }
}
