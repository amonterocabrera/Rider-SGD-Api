using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.Exceptions;
using SGDPEDIDOS.Application.Interfaces.Repositories;
using SGDPEDIDOS.Application.Interfaces.Services;
using SGDPEDIDOS.Application.Wrappers;
using SGDPEDIDOS.Domain.Entities;
using SGDPEDIDOS.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Infrastructure.Services
{
    public class FavoritesService : BaseService, IFavoritesService
    {
        private readonly IRepositoryAsync<Favorite> _FavoriteRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<FavoritesDto> _validator;
        public FavoritesService(IRepositoryAsync<Favorite> FavoriteRepo, IMapper mapper, IValidator<FavoritesDto> validator, IHttpContextAccessor context) : base(context)
        {
            _FavoriteRepo = FavoriteRepo;
            _mapper = mapper;
            _validator = validator;
        }

        private async Task<Response<Favorite>> ExitsAsync(int Id)
        {
            Favorite favorite = new Favorite();
            var userid = base.GetLoggerUserId();
            favorite = await _FavoriteRepo.WhereAsync(x => x.MenuId.Equals(Id) && x.UserId == userid && x.IsActive == true);

            return new Response<Favorite>(favorite);
        }

        public async Task<Response<FavoritesVm>> InsertAsync(FavoritesDto dto)
        {
            var userid = base.GetLoggerUserId();

            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            var obj = _mapper.Map<Favorite>(dto);
            obj.IsActive = true;
            obj.UserId = userid;

            return new Response<FavoritesVm>(_mapper.Map<FavoritesVm>(await _FavoriteRepo.AddAsync(obj)));

        }

        public async Task<Response<FavoritesVm>> DeleteAsync(FavoritesDto dto)
        {
            var userid = base.GetLoggerUserId();
            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            var objDb = await ExitsAsync(dto.MenuId);
            if (objDb == null)
            {
                throw new KeyNotFoundException($"Favorites not found by id={dto.MenuId}");
            }

            var obj = _mapper.Map<Favorite>(objDb.Data);
            obj.IsActive = false;

            return new Response<FavoritesVm>(_mapper.Map<FavoritesVm>(await _FavoriteRepo.UpdateAsync(obj)));
        }

        public async Task<Response<FavoritesVm>> GetByIdAsync(int id)
        {
            var data = await _FavoriteRepo.GetByIdAsync(id);
            if (data == null)
            {
                throw new KeyNotFoundException($"Favorites not found by id={id}");
            }

            return new Response<FavoritesVm>(_mapper.Map<FavoritesVm>(data));
        }



        public async Task<PagedResponse<IList<FavoritesDetalleVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null)
        {
            List<Expression<Func<Favorite, bool>>> queryFilter = new List<Expression<Func<Favorite, bool>>>();
            List<Expression<Func<Favorite, Object>>> includes = new List<Expression<Func<Favorite, Object>>>();

            var userLoginId = base.GetLoggerUserId();

            // Filtrar por el ID de usuario
            queryFilter.Add(x => x.UserId == userLoginId);

            if (!string.IsNullOrEmpty(filter))
            {
                queryFilter.Add(x => x.Menu.Name.Contains(filter) || x.Menu.MenuDescription.Contains(filter));
            }

            includes.Add(x => x.Menu);
            includes.Add(x => x.User);
            includes.Add(x => x.Menu.Company);
            includes.Add(x => x.Menu.TypeMenu);

            var list = await _FavoriteRepo.GetPagedList(pageNumber, pageSize, queryFilter, includes: includes);
            if (list == null || list.Data == null || list.Data.Count == 0)
            {
                throw new KeyNotFoundException("Favorites not found");
            }

            var favoritesDetalleVmList = list.Data.Select(f => new FavoritesDetalleVm
            {
                FavoriteId = f.FavoriteId,
                FullName = $"{f.User?.FirstName} {f.User?.SecondName} {f.User?.FirstLastName} {f.User?.SecondLastName}",
                MenuName = f.Menu?.Name,
                CompanyName = f.Menu?.Company?.CompanyName,
                TypeMenuName = f.Menu?.TypeMenu?.TypeMenuName,
                MenuAmount = f.Menu?.MenuAmount ?? 0,
                MenuDescription = f.Menu?.MenuDescription
            }).ToList();

            return new PagedResponse<IList<FavoritesDetalleVm>>(favoritesDetalleVmList, list.PageNumber, list.PageSize, list.TotalCount);
        }




    }
}
