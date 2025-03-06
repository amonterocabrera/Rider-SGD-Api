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
    [EnableCors("AllowWebapp")]
    public class SupplierCompanyController : OwnBaseController
    {
        private readonly ISupplierCompany _Service;
        public SupplierCompanyController(ISupplierCompany Service)
        {
            _Service = Service;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<SupplierCompanyVm>), 200)]
        public async Task<IActionResult> GetAsync(int id)
        { 
            return Ok(await _Service.GetByIdAsync(id));
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(PagedResponse<IList<SupplierCompanyVm>>), 200)]
        public async Task<IActionResult> GetAsync(int pageNumber, int pageSize, int SupplierId)
        {
            return Ok(await _Service.GetPagedListAsync(pageNumber, pageSize, SupplierId));
        }

        [HttpGet("GetAllSupplierCompany")]
        [ProducesResponseType(typeof(Response<SupplierCompanyViewVm>), 200)]
        public async Task<IActionResult> GetALLAsync(int? companyId)
        {
            return Ok(await _Service.GetAllAsync(companyId));
        }


        [HttpGet("GetAllCompany")]
        [ProducesResponseType(typeof(Response<SupplierCompanyViewVm>), 200)]
        public async Task<IActionResult> GetALLGetAllCompanyAsync()
        {
            return Ok(await _Service.GetALLGetAllCompanyAsync());
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<IList<SupplierCompanyVm>>), 200)]
        public async Task<IActionResult> PostAsync([FromBody] SupplierCompanyDto obj)
        {
            return Ok(await _Service.InsertAsync(obj));
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<IList<SupplierCompanyVm>>), 200)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SupplierCompanyDto obj)
        {
            return Ok(await _Service.UpdateAsync(obj.SupplierCompanyId, obj));
        }

    }
}
