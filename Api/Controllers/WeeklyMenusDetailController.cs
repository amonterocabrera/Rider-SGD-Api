using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SGDPEDIDOS.Api.Controllers.Base;
using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.Interfaces.Services;
using SGDPEDIDOS.Application.Wrappers;
using SGDPEDIDOS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAnyOrigin")]
    public class WeeklyMenusDetailController : OwnBaseController
    {
        private readonly IWeeklyMenusDetailService _WeeklyMenusDetailService;
        public WeeklyMenusDetailController(IWeeklyMenusDetailService WeeklyMenusDetailService)
        {
            _WeeklyMenusDetailService = WeeklyMenusDetailService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<WeeklyMenusDetailVm>), 200)]
        public async Task<IActionResult> GetAsync(int id)
        { 
            return Ok(await _WeeklyMenusDetailService.GetByIdAsync(id));
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(PagedResponse<IList<WeeklyMenusDetailVm>>), 200)]
        public async Task<IActionResult> GetAsync(int pageNumber, int pageSize, string filter = "")
        {
            return Ok(await _WeeklyMenusDetailService.GetPagedListAsync(pageNumber, pageSize, filter));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<IList<WeeklyMenusDetailVm>>), 200)]
        public async Task<IActionResult> PostAsync([FromBody] WeeklyMenusDetailDto obj)
        {
            return Ok(await _WeeklyMenusDetailService.InsertAsync(obj));
        }

        [HttpGet("weekmenuResult")]
        [ProducesResponseType(typeof(PagedResponse<IList<dynamic>>), 200)]
        public async Task<IActionResult> GetspAsync(int? weekly_menus_id)
        {
            return Ok(await _WeeklyMenusDetailService.sp_week_menuAsync(weekly_menus_id));
        }

        [HttpGet("getWeekAllBeforeMenus")]
        [ProducesResponseType(typeof(PagedResponse<IList<dynamic>>), 200)]
        public async Task<IActionResult> getWeekAllBeforeMenus(int weekly_menus_id)
        {
            return Ok(await _WeeklyMenusDetailService.getWeekAllBeforeMenus(weekly_menus_id));
        }

        [HttpGet("Menu_supplierAsync")]
        [ProducesResponseType(typeof(PagedResponse<IList<dynamic>>), 200)]
        public async Task<IActionResult> GetspMenusuppliersync(int? weekly_menus_id, int? weekly_day)
        {
            return Ok(await _WeeklyMenusDetailService.sp_view_week_menu_supplierAsync(weekly_menus_id, weekly_day));
        }

        [HttpPost("Insert_delete_week_menu_supplierAsync")]
        [ProducesResponseType(typeof(PagedResponse<IList<dynamic>>), 200)]
        public async Task<IActionResult> PostSpAllAsync(int? weekly_menus_id, int? weekly_day, int? menu_id, bool? is_active)
        {
            return Ok(await _WeeklyMenusDetailService.sp_insert_delete_week_menu_supplierAsync(weekly_menus_id,  weekly_day,  menu_id, is_active));
        }
    }
}
