using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SGDPEDIDOS.Api.Controllers.Base;
using SGDPEDIDOS.Application.DTOs.ViewModel.Recoverykey;
using SGDPEDIDOS.Application.Interfaces.Services;
using SGDPEDIDOS.Application.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Api.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAnyOrigin")]
    public class RecoverKeyController : OwnBaseController
    {
        private readonly IRecoverKeyService _RecuparClavesService;
        public RecoverKeyController(IRecoverKeyService RecuparClavesService)
        {
            _RecuparClavesService = RecuparClavesService;
        }

        //[HttpGet("{id}")]
        //[ProducesResponseType(typeof(Response<RecuperarClaveVm>), 200)]
        //public async Task<IActionResult> GetAsync(int id)
        //{
        //    return Ok(await _RecuparClavesService.GetByIdAsync(id));
        //}
        [AllowAnonymous]
        [HttpGet("")]
        [ProducesResponseType(typeof(PagedResponse<IList<RecoverKeyVm>>), 200)]
        public async Task<IActionResult> GetAsync(int pageNumber, int pageSize, string filter = "")
        {



            return Ok(await _RecuparClavesService.GetPagedListAsync(pageNumber, pageSize, filter));
        }
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(Response<IList<RecoverKeyVm>>), 200)]
        public async Task<IActionResult> PostAsync([FromBody] RecoverKeyViewModel obj)
        {
            return Ok(await _RecuparClavesService.InsertAsync(obj));
        }
        [AllowAnonymous]
        [HttpPost("verificarEnlace")]
        [ProducesResponseType(typeof(Response<IList<RecoverKeyVm>>), 200)]
        public async Task<IActionResult> VerificarUrl([FromBody] RecoverKeyViewModelurl obj)
        {
            return Ok(await _RecuparClavesService.VerificarKey(obj));
        }
        [AllowAnonymous]    
        [HttpPut]
        [ProducesResponseType(typeof(Response<IList<RecoverKeyVm>>), 200)]
        public async Task<IActionResult> PutAsync(RecoverKeyUpdate obj)
        {
            return Ok(await _RecuparClavesService.Actualizar( obj));
        }
    }
}
