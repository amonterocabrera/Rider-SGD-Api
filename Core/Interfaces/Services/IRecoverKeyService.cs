using SGDPEDIDOS.Application.DTOs.ViewModel.Recoverykey;
using SGDPEDIDOS.Application.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.Interfaces.Services
{
    public interface IRecoverKeyService
    {
        Task<Response<string>> InsertAsync(RecoverKeyViewModel dto);
        Task<Response<RecoverKeyVm>> Actualizar(RecoverKeyUpdate model);
        //Task<IActionResult> VerificarUrl(RecuperarClaveViewModelurl model)
        Task<int> VerificarKey(RecoverKeyViewModelurl model);
        Task<Response<RecoverKeyVm>> GetByIdAsync(int id);
        Task<PagedResponse<IList<RecoverKeyVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null);

    }
}
