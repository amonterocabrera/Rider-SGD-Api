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
    public class GendersController : OwnBaseController
    {
        private readonly IGenderService _GenderService;
        public GendersController(IGenderService GenderService)
        {
            _GenderService = GenderService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<GenderVm>), 200)]
        public async Task<IActionResult> GetAsync(int id)
        { 
            return Ok(await _GenderService.GetByIdAsync(id));
        }
        [HttpGet("GetAllGender")]
        [ProducesResponseType(typeof(Response<GenderVm>), 200)]
        public async Task<IActionResult> GetALLAsync()
        {
            return Ok(await _GenderService.GetAllAsync());
        }
        [HttpGet("")]
        [ProducesResponseType(typeof(PagedResponse<IList<GenderVm>>), 200)]
        public async Task<IActionResult> GetAsync(int pageNumber, int pageSize, string filter = "")
        {
            return Ok(await _GenderService.GetPagedListAsync(pageNumber, pageSize, filter));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<IList<GenderVm>>), 200)]
        public async Task<IActionResult> PostAsync([FromBody] GenderDto obj)
        {
            return Ok(await _GenderService.InsertAsync(obj));
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<IList<GenderVm>>), 200)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] GenderDto obj)
        {
            return Ok(await _GenderService.UpdateAsync(id, obj));
        }

    }
}
