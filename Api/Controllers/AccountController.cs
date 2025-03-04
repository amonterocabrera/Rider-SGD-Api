using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SGDPEDIDOS.application.DTOs.Security;
using SGDPEDIDOS.application.Interfaces.Services.Security;
using SGDPEDIDOS.Application.Wrappers;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAnyOrigin")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("LogIn")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public async Task<IActionResult> DoLogInAsync([FromBody] AuthenticationRequest obj)
        {
            return Ok(await _accountService.AuthenticateAsync(obj, GenerateIPAddress()));
        }

      

        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
