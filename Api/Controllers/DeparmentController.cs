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
    public class DeparmentController : OwnBaseController
    {
        private readonly IDeparmentServices _service;
        public DeparmentController(IDeparmentServices Service)
        {
            _service = Service;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<DeparmentVm>), 200)]
        public async Task<IActionResult> GetAsync(int id)
        { 
            return Ok(await _service.GetByIdAsync(id));
        }
        [HttpGet("GetAllDeparment")]
        [ProducesResponseType(typeof(Response<DeparmentVm>), 200)]
        public async Task<IActionResult> GetALLAsync()
        {
            return Ok(await _service.GetAllAsync());
        }
        [HttpGet("")]
        [ProducesResponseType(typeof(PagedResponse<IList<DeparmentVm>>), 200)]
        public async Task<IActionResult> GetAsync(int pageNumber, int pageSize, string filter = "")
        {
            return Ok(await _service.GetPagedListAsync(pageNumber, pageSize, filter));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<IList<DeparmentVm>>), 200)]
        public async Task<IActionResult> PostAsync([FromBody] DeparmentDto obj)
        {
            return Ok(await _service.InsertAsync(obj));
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<IList<DeparmentVm>>), 200)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] DeparmentDto obj)
        {
            return Ok(await _service.UpdateAsync(id, obj));
        }

    }
}
