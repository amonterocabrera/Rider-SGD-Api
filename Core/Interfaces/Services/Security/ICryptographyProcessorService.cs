using SGDPEDIDOS.application.DTOs.Security;

namespace SGDPEDIDOS.application.Interfaces.Services.Security
{
    public interface ICryptographyProcessorService
    {
        public PasswordAndSaltedStringInfo GetPasswordAndSecurityKeyInfo(string password);

        bool PasswordsMatch(string password, string securityKey, string HashedPassword);
    }
}
