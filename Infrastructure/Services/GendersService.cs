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
    public class GendersService : BaseService, IGenderService
    {
        private readonly IRepositoryAsync<Gender> _GenderRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<GenderDto> _validator;
        public GendersService(IRepositoryAsync<Gender> GenderRepo, IMapper mapper, IValidator<GenderDto> validator, IHttpContextAccessor context) : base(context)
        {
            _GenderRepo = GenderRepo;
            _mapper = mapper;
            _validator = validator;
        }

        private async Task<bool> ExitsAsync(int Id)
        {
            var result = await _GenderRepo.Exists(x => x.GenderId.Equals(Id));
            if (result) { return true; }
            throw new ApiException("Gender not found");
        }

        public async Task<Response<GenderVm>> InsertAsync(GenderDto dto)
        {

            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            var obj = _mapper.Map<Gender>(dto);

            obj.IsActive = true;


            return new Response<GenderVm>(_mapper.Map<GenderVm>(await _GenderRepo.AddAsync(obj)));

        }

        public async Task<Response<GenderVm>> UpdateAsync(int id, GenderDto dto)
        {
            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            await ExitsAsync(id);

            var objDb = await _GenderRepo.WhereAsync(x => x.GenderId.Equals(id));
            var obj = _mapper.Map<Gender>(dto);

            obj.GenderId = objDb.GenderId;



            return new Response<GenderVm>(_mapper.Map<GenderVm>(await _GenderRepo.UpdateAsync(obj)));
        }

        public async Task<Response<GenderVm>> GetByIdAsync(int id)
        {
            var data = await _GenderRepo.GetByIdAsync(id);
            if (data == null)
            {
                throw new KeyNotFoundException($"Gender not found by id={id}");
            }

            return new Response<GenderVm>(_mapper.Map<GenderVm>(data));
        }

        public async Task<Response<IList<GenderVm>>> GetAllAsync()
        {
            var list = await _GenderRepo.ListAllAsync();
            if (list == null)
            {
                throw new KeyNotFoundException($"Gender not found");
            }

            return new Response<IList<GenderVm>>(_mapper.Map<IList<GenderVm>>(list));
        }

        public async Task<PagedResponse<IList<GenderVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null)
        {

            List<Expression<Func<Gender, bool>>> queryFilter = new List<Expression<Func<Gender, bool>>>();
            List<Expression<Func<Gender, Object>>> includes = new List<Expression<Func<Gender, Object>>>();




            //if (filter != null || filter.Length > 0)
            //{
            //    queryFilter.Add(x => x.Id();
            //}

            var list = await _GenderRepo.GetPagedList(pageNumber, pageSize, queryFilter, includes: includes);
            if (list == null || list.Data.Count == 0)
            {
                throw new KeyNotFoundException($"Gender not found");
            }

            return new PagedResponse<IList<GenderVm>>(_mapper.Map<IList<GenderVm>>(list.Data), list.PageNumber, list.PageSize, list.TotalCount);
        }


    }
}
