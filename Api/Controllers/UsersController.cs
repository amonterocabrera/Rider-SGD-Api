using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SGDPEDIDOS.Api.Controllers.Base;
using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.Interfaces.Services;
using SGDPEDIDOS.Application.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Api.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAnyOrigin")]
    public class UsersController : OwnBaseController
    {
        private readonly IUsersService _usuariosService;
        public UsersController(IUsersService usuariosService)
        {
            _usuariosService = usuariosService;
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<UsersVm>), 200)]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await _usuariosService.GetByIdAsync(id));
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(PagedResponse<IList<UsersVm>>), 200)]
        public async Task<IActionResult> GetAsync(int pageNumber, int pageSize, string filter = "")
        {
            return Ok(await _usuariosService.GetPagedListAsync(pageNumber, pageSize, filter));
        }

        [HttpGet("GetUserListAdmin")]
        [ProducesResponseType(typeof(PagedResponse<IList<UsersVm>>), 200)]
        public async Task<IActionResult> GetUserListAdmin(int pageNumber, int pageSize, int companyId, int userTypeId, string filter = "")
        {
            return Ok(await _usuariosService.GetUserListAdmin(pageNumber, pageSize, companyId, userTypeId, filter));
        }



        [HttpPost]
        [ProducesResponseType(typeof(Response<IList<UsersVm>>), 200)]
        public async Task<IActionResult> PostAsync([FromBody] UsersDto obj)
        {
            return Ok(await _usuariosService.InsertAsync(obj));
        }

        [HttpPost("UpdateBalanceListAsync")]
        [ProducesResponseType(typeof(Response<IList<UsersVm>>), 200)]
        public async Task<IActionResult> UpdateBalanceListAsync(List<CreditAvailableDtoList> obj)
        {
            return Ok(await _usuariosService.UpdateBalanceListAsync(obj));
        }


        [HttpPost("InsertAsyncList")]
        [ProducesResponseType(typeof(Response<IList<UsersVm>>), 200)]
        public async Task<IActionResult> InsertAsyncList([FromBody] List<UsersDtoList> obj)
        {
            return Ok(await _usuariosService.InsertListAsync(obj));
        }

        
        [HttpPost("userImagen")]
        [ProducesResponseType(typeof(Response<IList<UsersVm>>), 200)]
        public async Task<IActionResult> PostImagenAsync([FromBody] UsersImagenDto obj)
        {
            return Ok(await _usuariosService.InsertImagenAsync(obj));
        }

        [HttpPut("UpdateUserAsync")]
        [ProducesResponseType(typeof(Response<IList<UsersVm>>), 200)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] UsersDto obj)
        {
            return Ok(await _usuariosService.UpdateAsync(id, obj));
        }
        [HttpPut("ChancePassword")]
        [ProducesResponseType(typeof(Response<IList<UsersVm>>), 200)]
        public async Task<IActionResult> PutPasswordAsync(int id, [FromBody] UpdatePasswordUsersDto obj)
        {
            return Ok(await _usuariosService.UpdatePasswordAsync(id, obj));
        }


    }

}
