using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SGDPEDIDOS.Api.Controllers.Base;
using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.Interfaces.Services;
using SGDPEDIDOS.Application.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DSCALIDAD.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAnyOrigin")]
    public class FavoritesController : OwnBaseController
    {
        private readonly IFavoritesService _FavoritesService;
        public FavoritesController(IFavoritesService FavoritesService)
        {
            _FavoritesService = FavoritesService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<FavoritesVm>), 200)]
        public async Task<IActionResult> GetAsync(int id)
        { 
            return Ok(await _FavoritesService.GetByIdAsync(id));
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(PagedResponse<IList<FavoritesDetalleVm>>), 200)]
        public async Task<IActionResult> GetAsync(int pageNumber, int pageSize, string filter = "")
        {
            return Ok(await _FavoritesService.GetPagedListAsync(pageNumber, pageSize, filter));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<IList<FavoritesVm>>), 200)]
        public async Task<IActionResult> PostAsync([FromBody] FavoritesDto obj)
        {
            return Ok(await _FavoritesService.InsertAsync(obj));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(Response<IList<FavoritesVm>>), 200)]
        public async Task<IActionResult> DeleteAsync([FromBody] FavoritesDto obj)
        {
            return Ok(await _FavoritesService.DeleteAsync(obj));
        }

    }
}
