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
    public class WeeklyDaysController : OwnBaseController
    {
        private readonly IWeeklyDaysService _WeeklyDaysService;
        public WeeklyDaysController(IWeeklyDaysService WeeklyDaysService)
        {
            _WeeklyDaysService = WeeklyDaysService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<WeeklyDaysVm>), 200)]
        public async Task<IActionResult> GetAsync(int id)
        { 
            return Ok(await _WeeklyDaysService.GetByIdAsync(id));
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(PagedResponse<IList<WeeklyDaysVm>>), 200)]
        public async Task<IActionResult> GetAsync(int pageNumber, int pageSize, string filter = "")
        {
            return Ok(await _WeeklyDaysService.GetPagedListAsync(pageNumber, pageSize, filter));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<IList<WeeklyDaysVm>>), 200)]
        public async Task<IActionResult> PostAsync([FromBody] WeeklyDaysDto obj)
        {
            return Ok(await _WeeklyDaysService.InsertAsync(obj));
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<IList<WeeklyDaysVm>>), 200)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] WeeklyDaysDto obj)
        {
            return Ok(await _WeeklyDaysService.UpdateAsync(id, obj));
        }

    }
}
