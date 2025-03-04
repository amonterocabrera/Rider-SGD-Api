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
    public class WeeklyMenusController : OwnBaseController
    {
        private readonly IWeeklyMenusService _WeeklyMenusService;
        public WeeklyMenusController(IWeeklyMenusService WeeklyMenusService)
        {
            _WeeklyMenusService = WeeklyMenusService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<WeeklyMenusVm>), 200)]
        public async Task<IActionResult> GetAsync(int id)
        { 
            return Ok(await _WeeklyMenusService.GetByIdAsync(id));
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(PagedResponse<IList<WeeklyMenusVm>>), 200)]
        public async Task<IActionResult> GetAsync(int pageNumber, int pageSize, int? companyId)
        {
            return Ok(await _WeeklyMenusService.GetPagedListAsync(pageNumber, pageSize, companyId));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<IList<WeeklyMenusVm>>), 200)]
        public async Task<IActionResult> PostAsync([FromBody] WeeklyMenusDto obj)
        {
            return Ok(await _WeeklyMenusService.InsertAsync(obj));
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<IList<WeeklyMenusVm>>), 200)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] WeeklyMenusDto obj)
        {
            return Ok(await _WeeklyMenusService.UpdateAsync(id, obj));
        }

    }
}
