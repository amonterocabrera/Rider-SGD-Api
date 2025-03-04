using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.Interfaces.Services
{
    public interface ITypeSuggestionsService
    {
        Task<Response<TypeSuggestionsVm>> InsertAsync(TypeSuggestionsDto dto);

        Task<Response<TypeSuggestionsVm>> UpdateAsync(int id, TypeSuggestionsDto dto);


        Task<Response<TypeSuggestionsVm>> GetByIdAsync(int id);
        Task<Response<IList<TypeSuggestionsVm>>> GetAllAsync();

        Task<PagedResponse<IList<TypeSuggestionsVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null);

    } }
