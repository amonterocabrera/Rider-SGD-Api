using Microsoft.IdentityModel.Tokens;
using SGDPEDIDOS.application.DTOs.Security;
using SGDPEDIDOS.application.DTOs.Settings;
using SGDPEDIDOS.application.Interfaces.Services.Security;
using SGDPEDIDOS.Application.Exceptions;
using SGDPEDIDOS.Application.Interfaces.Repositories;
using SGDPEDIDOS.Application.Wrappers;
using SGDPEDIDOS.Domain.Entities;
using SGDPEDIDOS.Identity.Helper;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly JWTSettings _jwtSettings;
        private readonly ICryptographyProcessorService _cryptographyProcessorService;
        private readonly IRepositoryAsync<User> _userRepo;
        private readonly IRepositoryAsync<Company> _CompanyRepo;
        private readonly IRepositoryAsync<UsersType> _UsersTypeRepo;
        public AccountService(JWTSettings jwtSettings, ICryptographyProcessorService cryptographyProcessorService, IRepositoryAsync<User> userRepo, IRepositoryAsync<Company> CompanyRepo, IRepositoryAsync<UsersType> UsersTypeRepo)
        {
            _jwtSettings = jwtSettings;
            _cryptographyProcessorService = cryptographyProcessorService;
            _userRepo = userRepo;
            _CompanyRepo = CompanyRepo;
            _UsersTypeRepo = UsersTypeRepo;


        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {

            var userData = await _userRepo.WhereAllAsync(x => x.Email == request.Email);
            var usuario = userData.LastOrDefault();
            if (usuario == null )
            {
                throw new ApiException($"Usuario {request.Email} no encontrado.");
            }
            if (usuario.IsActive == false)
            {
                throw new ApiException($"Usuario {request.Email} Esta Desactivado.");
            }
            var PasswordsMatchResult = _cryptographyProcessorService.PasswordsMatch(request.Password, usuario.UserSecurityKey, usuario.Password);
            if (!PasswordsMatchResult)
            {
                throw new ApiException($"Datos invalidos");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateJWTToken(usuario);
            AuthenticationResponse response = new AuthenticationResponse();
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.Expiration = DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes);

            usuario.LastAccess =  DateTime.Now;
            
            await _userRepo.UpdateAsync(usuario);   
            
            
            return new Response<AuthenticationResponse>(response, $"Authenticate {usuario.Email}");
        }


        private async Task<JwtSecurityToken> GenerateJWTToken(User usuario)
        {
            

            var company = _CompanyRepo.WhereAsync(x => x.CompanyId == usuario.CompanyId).Result;
        
            var usertype = _UsersTypeRepo.WhereAsync(x=> x.UserTypeId == usuario.UserTypeId).Result;

            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("email", usuario.Email),
                new Claim("companyId", usuario.CompanyId.ToString()),
                new Claim("companyName", company.CompanyName),
                new Claim("ip", ipAddress),
                new Claim("Fullname", $"{usuario.FirstName} {usuario.SecondName} {usuario.FirstLastName} {usuario.SecondLastName}"),
                new Claim("uid", usuario.UserId.ToString()),
                new Claim("userTypeName",  usertype.UserTypeName),
                new Claim("userTypeId",  usertype.UserTypeId.ToString()),
             };
            //.Union(roleClaims);


            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
                );

            return jwtSecurityToken;
        }

    }
}
