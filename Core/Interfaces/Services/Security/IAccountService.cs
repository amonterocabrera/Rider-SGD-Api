using SGDPEDIDOS.application.DTOs.Security;
using SGDPEDIDOS.Application.Wrappers;
using System.Threading.Tasks;

namespace SGDPEDIDOS.application.Interfaces.Services.Security
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);

    }
}
