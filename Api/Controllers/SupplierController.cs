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
    public class SupplierController : OwnBaseController
    {
        private readonly ISupplierServices _Service;
        public SupplierController(ISupplierServices CompanyService)
        {
            _Service = CompanyService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<CompanyVm>), 200)]
        public async Task<IActionResult> GetAsync(int id)
        { 
            return Ok(await _Service.GetByIdAsync(id));
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(PagedResponse<IList<CompanyVm>>), 200)]
        public async Task<IActionResult> GetAsync(int pageNumber, int pageSize, int? SupplierId)
        {
            return Ok(await _Service.GetPagedListAsync(pageNumber, pageSize, SupplierId));
        }
        [HttpGet("ReportSupplier")]
        [ProducesResponseType(typeof(PagedResponse<IList<CompanyVm>>), 200)]
        public async Task<IActionResult> GetReportAsync(int pageNumber, int pageSize, int? SupplierId)
        {
            return Ok(await _Service.GetPagedListReportAsync(pageNumber, pageSize, SupplierId));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<IList<CompanyVm>>), 200)]
        public async Task<IActionResult> PostAsync([FromBody] CompanyDto obj)
        {
            return Ok(await _Service.InsertAsync(obj));
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<IList<CompanyVm>>), 200)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] CompanyDto obj)
        {
            return Ok(await _Service.UpdateAsync(id, obj));
        }

    }
}
