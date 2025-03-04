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
    public class TypesIdentificationsController : OwnBaseController
    {
        private readonly ITypesIdentificationService _TypesIdentificationService;
        public TypesIdentificationsController(ITypesIdentificationService TypesIdentificationService)
        {
            _TypesIdentificationService = TypesIdentificationService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<TypesIdentificationsVm>), 200)]
        public async Task<IActionResult> GetAsync(int id)
        { 
            return Ok(await _TypesIdentificationService.GetByIdAsync(id));
        }
        [HttpGet("GetAllTypesIdentifications")]
        [ProducesResponseType(typeof(Response<TypesIdentificationsVm>), 200)]
        public async Task<IActionResult> GetALLAsync()
        {
            return Ok(await _TypesIdentificationService.GetAllAsync());
        }
        [HttpGet("")]
        [ProducesResponseType(typeof(PagedResponse<IList<TypesIdentificationsVm>>), 200)]
        public async Task<IActionResult> GetAsync(int pageNumber, int pageSize, string filter = "")
        {
            return Ok(await _TypesIdentificationService.GetPagedListAsync(pageNumber, pageSize, filter));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<IList<TypesIdentificationsVm>>), 200)]
        public async Task<IActionResult> PostAsync([FromBody] TypesIdentificationsDto obj)
        {
            return Ok(await _TypesIdentificationService.InsertAsync(obj));
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<IList<TypesIdentificationsVm>>), 200)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] TypesIdentificationsDto obj)
        {
            return Ok(await _TypesIdentificationService.UpdateAsync(id, obj));
        }

    }
}
