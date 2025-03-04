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
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DSCALIDAD.Infrastructure.Services
{
    public class WeeklyMenusService : BaseService, IWeeklyMenusService
    {
        private readonly IRepositoryAsync<WeeklyMenu> _WeeklyMenuRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<WeeklyMenusDto> _validator;
        public WeeklyMenusService(IRepositoryAsync<WeeklyMenu>  WeeklyMenuRepo, IMapper mapper, IValidator<WeeklyMenusDto> validator, IHttpContextAccessor context) : base(context)
        {
            _WeeklyMenuRepo = WeeklyMenuRepo;
            _mapper = mapper;
            _validator = validator;
        }

        private async Task<bool> ExitsAsync(int Id)
        {
            var result = await _WeeklyMenuRepo.Exists(x => x.WeeklyMenusId.Equals(Id));
            if (result) { return true; }
            throw new ApiException("WeeklyMenus not found");
        }

        public async Task<Response<WeeklyMenusVm>> InsertAsync(WeeklyMenusDto dto)
        {

            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            var obj = _mapper.Map<WeeklyMenu>(dto);


            return new Response<WeeklyMenusVm>(_mapper.Map<WeeklyMenusVm>(await _WeeklyMenuRepo.AddAsync(obj)));

        }

        public async Task<Response<WeeklyMenusVm>> UpdateAsync(int id, WeeklyMenusDto dto)
        {
            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            await ExitsAsync(id);

            var objDb = await _WeeklyMenuRepo.WhereAsync(x => x.WeeklyMenusId.Equals(id));
            var obj = _mapper.Map<WeeklyMenu>(dto);

            obj.WeeklyMenusId = objDb.WeeklyMenusId;


            return new Response<WeeklyMenusVm>(_mapper.Map<WeeklyMenusVm>(await _WeeklyMenuRepo.UpdateAsync(obj)));
        }

        public async Task<Response<WeeklyMenusVm>> GetByIdAsync(int id)
        {
            var data = await _WeeklyMenuRepo.GetByIdAsync(id);
            if (data == null)
            {
                throw new KeyNotFoundException($"WeeklyMenus not found by id={id}");
            }

            return new Response<WeeklyMenusVm>(_mapper.Map<WeeklyMenusVm>(data));
        }



        public async Task<PagedResponse<IList<WeeklyMenusVm>>> GetPagedListAsync(int pageNumber, int pageSize, int? SupplierId)
        {

            List<Expression<Func<WeeklyMenu, bool>>> queryFilter = new List<Expression<Func<WeeklyMenu, bool>>>();
            List<Expression<Func<WeeklyMenu, Object>>> includes = new List<Expression<Func<WeeklyMenu, Object>>>();



            if (SupplierId != null || SupplierId > 0)
            {
                //queryFilter.Add(x => x.);
            }

            var list = await _WeeklyMenuRepo.GetPagedList(pageNumber, pageSize, queryFilter, includes: includes);
            if (list == null || list.Data.Count == 0)
            {
                throw new KeyNotFoundException($"WeeklyMenus not found");
            }

            return new PagedResponse<IList<WeeklyMenusVm>>(_mapper.Map<IList<WeeklyMenusVm>>(list.Data), list.PageNumber, list.PageSize, list.TotalCount);
        }


    }
}
