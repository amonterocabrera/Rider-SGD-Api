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
    public class CompanyController : OwnBaseController
    {
        private readonly ICompanyService _CompanyService;
        public CompanyController(ICompanyService CompanyService)
        {
            _CompanyService = CompanyService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<CompanyVm>), 200)]
        public async Task<IActionResult> GetByIdAsync(int id)
        { 
            return Ok(await _CompanyService.GetByIdAsync(id));
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(PagedResponse<IList<CompanyVm>>), 200)]
        public async Task<IActionResult> GetPagedListAsync(int pageNumber, int pageSize, string filter = "")
        {
            return Ok(await _CompanyService.GetPagedListAsync(pageNumber, pageSize, filter));
        }

        [HttpGet("GetAllPagedListAsync")]
        [ProducesResponseType(typeof(PagedResponse<IList<CompanyVm>>), 200)]
        public async Task<IActionResult> GetAllPagedListAsync(int pageNumber, int pageSize, string filter = "", int? companyType = 0)
        {
            return Ok(await _CompanyService.GetAllPagedListAsync(pageNumber, pageSize, filter, companyType));
        }

        [HttpGet("GetAllListAsync")]
        [ProducesResponseType(typeof(PagedResponse<IList<CompanyVm>>), 200)]
        public async Task<IActionResult> GetAllListAsync([FromQuery]int typeCompanyId)
        {
            return Ok(await _CompanyService.GetAllListAsync(typeCompanyId));
        }

        [HttpGet("GetAllCompaniesAdmin")]
        [ProducesResponseType(typeof(PagedResponse<IList<CompanyVm>>), 200)]
        public async Task<IActionResult> GetAllCompaniesAdmin([FromQuery] int typeCompanyId)
        {
            return Ok(await _CompanyService.GetAllCompaniesAdmin(typeCompanyId));
        }


        [HttpPost]
        [ProducesResponseType(typeof(Response<IList<CompanyVm>>), 200)]
        public async Task<IActionResult> PostAsync([FromBody] CompanyDto obj)
        {
            return Ok(await _CompanyService.InsertAsync(obj));
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<IList<CompanyVm>>), 200)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] CompanyDto obj)
        {
            return Ok(await _CompanyService.UpdateAsync(id, obj));
        }

     

    }
}
