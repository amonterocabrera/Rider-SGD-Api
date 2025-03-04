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
    public class WeeklyDaysService : BaseService, IWeeklyDaysService
    {
        private readonly IRepositoryAsync<WeeklyDay> _WeeklyDayRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<WeeklyDaysDto> _validator;
        public WeeklyDaysService(IRepositoryAsync<WeeklyDay> WeeklyDayRepo, IMapper mapper, IValidator<WeeklyDaysDto> validator, IHttpContextAccessor context) : base(context)
        {
            _WeeklyDayRepo = WeeklyDayRepo;
            _mapper = mapper;
            _validator = validator;
        }

        private async Task<bool> ExitsAsync(int Id)
        {
            var result = await _WeeklyDayRepo.Exists(x => x.WeeklyDayId.Equals(Id));
            if (result) { return true; }
            throw new ApiException("WeeklyDay not found");
        }

        public async Task<Response<WeeklyDaysVm>> InsertAsync(WeeklyDaysDto dto)
        {

            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            var obj = _mapper.Map<WeeklyDay>(dto);


            return new Response<WeeklyDaysVm>(_mapper.Map<WeeklyDaysVm>(await _WeeklyDayRepo.AddAsync(obj)));

        }

        public async Task<Response<WeeklyDaysVm>> UpdateAsync(int id, WeeklyDaysDto dto)
        {
            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            await ExitsAsync(id);

            var objDb = await _WeeklyDayRepo.WhereAsync(x => x.WeeklyDayId.Equals(id));
            var obj = _mapper.Map<WeeklyDay>(dto);

            obj.WeeklyDayId = objDb.WeeklyDayId;


            return new Response<WeeklyDaysVm>(_mapper.Map<WeeklyDaysVm>(await _WeeklyDayRepo.UpdateAsync(obj)));
        }

        public async Task<Response<WeeklyDaysVm>> GetByIdAsync(int id)
        {
            var data = await _WeeklyDayRepo.GetByIdAsync(id);
            if (data == null)
            {
                throw new KeyNotFoundException($"WeeklyDay not found by id={id}");
            }

            return new Response<WeeklyDaysVm>(_mapper.Map<WeeklyDaysVm>(data));
        }



        public async Task<PagedResponse<IList<WeeklyDaysVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null)
        {

            List<Expression<Func<WeeklyDay, bool>>> queryFilter = new List<Expression<Func<WeeklyDay, bool>>>();
            List<Expression<Func<WeeklyDay, Object>>> includes = new List<Expression<Func<WeeklyDay, Object>>>();




            //if (filter != null || filter.Length > 0)
            //{
            //    queryFilter.Add(x => x.Id();
            //}

            var list = await _WeeklyDayRepo.GetPagedList(pageNumber, pageSize, queryFilter, includes: includes);
            if (list == null || list.Data.Count == 0)
            {
                throw new KeyNotFoundException($"WeeklyDay not found");
            }

            return new PagedResponse<IList<WeeklyDaysVm>>(_mapper.Map<IList<WeeklyDaysVm>>(list.Data), list.PageNumber, list.PageSize, list.TotalCount);
        }


    }
}
