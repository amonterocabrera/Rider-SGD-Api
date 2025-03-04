using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SGDPEDIDOS.Api.Controllers.Base;
using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.Interfaces.Services;
using SGDPEDIDOS.Application.Wrappers;
using SGDPEDIDOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAnyOrigin")]
    public class MenusController : OwnBaseController
    {
        private readonly IMenusService _MenusService;
        public MenusController(IMenusService MenusService)
        {
            _MenusService = MenusService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<MenusVm>), 200)]
        public async Task<IActionResult> GetAsync(int id)
        { 
            return Ok(await _MenusService.GetByIdAsync(id));
        }

        [HttpGet("GetPagedListAsyncCollaborator")]
        [ProducesResponseType(typeof(PagedResponse<IList<MenuCompany>>), 200)]
        public async Task<IActionResult> GetAsync(int pageNumber, int pageSize, int? SupplierId =0)
        {
            int weeklyMenusId = 0;
            
            return Ok(await _MenusService.GetPagedListAsync(pageNumber, pageSize,  weeklyMenusId,Convert.ToInt32(SupplierId)));
        }
        [HttpGet("GeActiveInactiveListAsyncSupplier")]
        [ProducesResponseType(typeof(PagedResponse<IList<MenuCompany>>), 200)]
        public async Task<IActionResult> GeActiveInactiveListAsync(int pageNumber, int pageSize,  int weeklyMenuId, int weeklyDayId, bool? IsActive)
        {
            return Ok(await _MenusService.GeActiveInactiveListAsync(pageNumber, pageSize, weeklyMenuId, weeklyDayId, IsActive));
        }

        [HttpGet("GeActiveInactiveListAllPaginate")]
        [ProducesResponseType(typeof(PagedResponse<MenuCompany>), 200)]
        public async Task<IActionResult> GeActiveInactiveListAllPaginate(int pageNumber, int pageSize, string searchText, bool IsActive, int type)
        {
            return Ok(await _MenusService.GeActiveInactiveListAllPaginate(pageNumber, pageSize,  searchText, IsActive, type));
        }

        [HttpGet("GeActiveInactiveListAll")]
        [ProducesResponseType(typeof(PagedResponse<IList<MenuCompany>>), 200)]
        public async Task<IActionResult> GeActiveInactiveListAll()
        {
            return Ok(await _MenusService.GeActiveInactiveListAll());
        }


        [HttpPost("CreateDishes")]
        [ProducesResponseType(typeof(Response<IList<MenusVm>>), 200)]
        public async Task<IActionResult> CreateDishes([FromBody] MenusDto obj)
        {
            return Ok(await _MenusService.CreateDishes(obj));
        }
        [HttpPost("UpdateDishes")]
        [ProducesResponseType(typeof(Response<IList<MenusVm>>), 200)]
        public async Task<IActionResult> UpdateDishes([FromBody] MenusDto obj)
        {
            return Ok(await _MenusService.UpdateDishes(obj));
        }

        [HttpPost("AddandRemovePitucture")]
        [ProducesResponseType(typeof(Response<bool>), 200)]
        public async Task<IActionResult> AddandRemovePitucture([FromBody] DtoAddRemoveImage obj)
        {
            return Ok(await _MenusService.AddandRemovePitucture(obj));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<IList<MenusVm>>), 200)]
        public async Task<IActionResult> PostAsync([FromBody] MenusDto obj)
        {
            return Ok(await _MenusService.InsertAsync(obj));
        }

        [HttpPost("CopyMenutoMenuAsync")]
        [ProducesResponseType(typeof(Response<bool>), 200)]
        public async Task<IActionResult> CopyMenuAsync([FromBody] CopyMenusDto obj)
        {
            return Ok(await _MenusService.CopyMenuAsync(obj));
        }

        [HttpPut]
        [ProducesResponseType(typeof(Response<IList<MenusVm>>), 200)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] MenusDto obj)
        {
            return Ok(await _MenusService.UpdateAsync(id, obj));
        }

        [HttpPut("ActiveInactive")]
        [ProducesResponseType(typeof(Response<IList<MenusVm>>), 200)]
        public async Task<IActionResult> ActiveInactive(int MenuId, bool IsActive)
        {
            return Ok(await _MenusService.ActiveInactive(MenuId, IsActive));
        }

        [HttpDelete("DeleteAsync")]
        [ProducesResponseType(typeof(Response<IList<MenusVm>>), 200)]
        public async Task<IActionResult> DeleteAsync(int MenuId)
        {
            return Ok(await _MenusService.DeleteAsync(MenuId));
        }


        //[HttpGet("GeActiveInactiveList")]
        //[ProducesResponseType(typeof(PagedResponse<IList<MenuCompany>>), 200)]
        //public async Task<IActionResult> GetActiveInactivePagedMenusAsync(int pageNumber, int pageSize, int weeklyMenuId, int weeklyDayId)
        //{
        //    return Ok(await _MenusService.GetActiveInactivePagedMenusAsync(pageNumber, pageSize, weeklyMenuId, weeklyDayId));
        //}

    }
}
