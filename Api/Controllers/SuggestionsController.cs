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
    public class SuggestionsController : OwnBaseController
    {
        private readonly ISuggestionsService _SuggestionsService;
        public SuggestionsController(ISuggestionsService SuggestionsService)
        {
            _SuggestionsService = SuggestionsService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<SuggestionsVm>), 200)]
        public async Task<IActionResult> GetAsync(int id)
        { 
            return Ok(await _SuggestionsService.GetByIdAsync(id));
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(PagedResponse<IList<SuggestionsVm>>), 200)]
        public async Task<IActionResult> GetAsync(int pageNumber, int pageSize, string filter = "")
        {
            return Ok(await _SuggestionsService.GetPagedListAsync(pageNumber, pageSize, filter));
        }

        [HttpGet("ReportSuggestion")]
        [ProducesResponseType(typeof(PagedResponse<IList<SuggestionsVm>>), 200)]
        public async Task<IActionResult> GetReportAsync(int pageNumber, int pageSize, string filter = "")
        {
            return Ok(await _SuggestionsService.GetPagedListReportAsync(pageNumber, pageSize, filter));
        }
        [HttpPost]
        [ProducesResponseType(typeof(Response<IList<SuggestionsVm>>), 200)]
        public async Task<IActionResult> PostAsync([FromBody] SuggestionsDto obj)
        {
            return Ok(await _SuggestionsService.InsertAsync(obj));
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<IList<SuggestionsVm>>), 200)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SuggestionsDto obj)
        {
            return Ok(await _SuggestionsService.UpdateAsync(id, obj));
        }

    }
}
