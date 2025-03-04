using DSCALIDAD.Infrastructure.Services;
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
    public class UsersTypesController : OwnBaseController
    {
        private readonly IUsersTypesService _UsersTypesService;
        public UsersTypesController(IUsersTypesService UsersTypesService)
        {
            _UsersTypesService = UsersTypesService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<UsersTypesVm>), 200)]
        public async Task<IActionResult> GetAsync(int id)
        { 
            return Ok(await _UsersTypesService.GetByIdAsync(id));
        }
        [HttpGet("GetAllUsersTypes")]
        [ProducesResponseType(typeof(Response<UsersTypesVm>), 200)]
        public async Task<IActionResult> GetALLAsync()
        {
            return Ok(await _UsersTypesService.GetAllAsync());
        }
        [HttpGet("")]
        [ProducesResponseType(typeof(PagedResponse<IList<UsersTypesVm>>), 200)]
        public async Task<IActionResult> GetAsync(int pageNumber, int pageSize, string filter = "")
        {
            return Ok(await _UsersTypesService.GetPagedListAsync(pageNumber, pageSize, filter));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<IList<UsersTypesVm>>), 200)]
        public async Task<IActionResult> PostAsync([FromBody] UsersTypesDto obj)
        {
            return Ok(await _UsersTypesService.InsertAsync(obj));
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<IList<UsersTypesVm>>), 200)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] UsersTypesDto obj)
        {
            return Ok(await _UsersTypesService.UpdateAsync(id, obj));
        }

    }
}
