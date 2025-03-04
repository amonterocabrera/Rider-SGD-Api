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
    public class TypeSuggestionsController : OwnBaseController
    {
        private readonly ITypeSuggestionsService _TypeSuggestionsService;
        public TypeSuggestionsController(ITypeSuggestionsService TypeSuggestionsService)
        {
            _TypeSuggestionsService = TypeSuggestionsService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<TypeSuggestionsVm>), 200)]
        public async Task<IActionResult> GetAsync(int id)
        { 
            return Ok(await _TypeSuggestionsService.GetByIdAsync(id));
        }

      
        [HttpGet("GetAllTypeSuggestions")]
        [ProducesResponseType(typeof(Response<TypeSuggestionsVm>), 200)]
        public async Task<IActionResult> GetALLAsync()
        {
            return Ok(await _TypeSuggestionsService.GetAllAsync());
        }
        [HttpGet("")]
        [ProducesResponseType(typeof(PagedResponse<IList<TypeSuggestionsVm>>), 200)]
        public async Task<IActionResult> GetAsync(int pageNumber, int pageSize, string filter = "")
        {
            return Ok(await _TypeSuggestionsService.GetPagedListAsync(pageNumber, pageSize, filter));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<IList<TypeSuggestionsVm>>), 200)]
        public async Task<IActionResult> PostAsync([FromBody] TypeSuggestionsDto obj)
        {
            return Ok(await _TypeSuggestionsService.InsertAsync(obj));
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<IList<TypeSuggestionsVm>>), 200)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] TypeSuggestionsDto obj)
        {
            return Ok(await _TypeSuggestionsService.UpdateAsync(id, obj));
        }

    }
}
