using DSCALIDAD.Infrastructure.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SGDPEDIDOS.Api.Models;
using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.Interfaces.Services;
using SGDPEDIDOS.Application.Wrappers;
using SGDPEDIDOS.Domain.Entities.View;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAnyOrigin")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        

        public InvoicesController(IInvoiceService invoiceService, IConfiguration configuration)
        {
            _invoiceService = invoiceService;
            
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<InvoiceVm>), 200)]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(await _invoiceService.GetByIdAsync(id));
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(PagedResponse<IList<InvoiceVm>>), 200)]
        public async Task<IActionResult> GetAsync(int pageNumber, int pageSize, int orderStatusId)
        {
            return Ok(await _invoiceService.GetPagedListAsync(pageNumber, pageSize, orderStatusId));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<IList<InvoiceVm>>), 200)]
        public async Task<IActionResult> PostAsync([FromBody] List<InvoiceDto> obj)
        {
            return Ok(await _invoiceService.InsertAsync(obj));
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<IList<InvoiceVm>>), 200)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] InvoiceDto obj)
        {
            return Ok(await _invoiceService.UpdateAsync(id, obj));
        }


        [HttpPut("ChangeStatus")]
        [ProducesResponseType(typeof(Response<IList<InvoiceVm>>), 200)]
        public async Task<IActionResult> ChangeStatus(int id, [FromBody] InvoiceStatusDto obj)
        {
            return Ok(await _invoiceService.ChangeStatus(id, obj));
        }

        [HttpGet("Report")]
        [ProducesResponseType(typeof(PagedResponse<IList<VReportVm>>), 200)]
        public async Task<IActionResult> GetViewAsync(int pageNumber, int pageSize, int Day, int SupplierId, string filter = "")
        {
            return Ok(await _invoiceService.GetPagedViewListAsync( pageNumber,  pageSize,  Day, SupplierId, filter));
        }
        [HttpGet("GetAllInvoicesOrders")]
        [ProducesResponseType(typeof(PagedResponse<IList<VReportVm>>), 200)]
        public async Task<IActionResult> GetViewCompanyAsync(int pageNumber, int pageSize, string Day, int SupplierId, int statusId, string filter = "")
        {
            return Ok(await _invoiceService.GetPagedViewCompanyListAsync(pageNumber, pageSize, Day, SupplierId, statusId,filter));
        }

        [HttpGet("GetPagedViewSuppliedDetailsList")]
        [ProducesResponseType(typeof(PagedResponse<IList<VReportSupplier>>), 200)]
        public async Task<IActionResult> GetViewSuppliedAsync(int pageNumber, int pageSize, int supplierId, int selectedMonth, int selectedYear)
        {
            return Ok(await _invoiceService.GetPagedViewSuppliedDetailsListAsync( pageNumber,  pageSize,  supplierId,  selectedMonth,  selectedYear));
        }

        [HttpGet("GetPagedViewGroupList")]
        [ProducesResponseType(typeof(PagedResponse<IList<VReportSupplier>>), 200)]
        public async Task<IActionResult> GetPagedViewGroupListAsync(int pageNumber, int pageSize, int supplierId, int selectedMonth,  int selectedYear)
        {
            return Ok(await _invoiceService.GetPagedViewGroupListAsync(pageNumber, pageSize, supplierId, selectedMonth, selectedYear));
        }

        //[HttpGet("GetPagedViewSuppliedDetailsListSupplier")]
        //[ProducesResponseType(typeof(PagedResponse<IList<VReportSupplier>>), 200)]
        //public async Task<IActionResult> GetPagedViewSuppliedDetailsListSupplier(int pageNumber, int pageSize, string Day, int EmpresaId, string filter = "")
        //{
        //    return Ok(await _invoiceService.GetPagedViewSuppliedDetailsListSupplier(pageNumber, pageSize, Day, EmpresaId, filter));
        //}

        [HttpGet("GetPagedViewGroupListSupplier")]
        [ProducesResponseType(typeof(PagedResponse<IList<VReportSupplier>>), 200)]
        public async Task<IActionResult> GetPagedViewGroupListSupplier(int pageNumber, int pageSize, string Day, int EmpresaId, string filter = "")
        {
            return Ok(await _invoiceService.GetPagedViewGroupListSupplier(pageNumber, pageSize, Day, EmpresaId, filter));
        }



    }
}
