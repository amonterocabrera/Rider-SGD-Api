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
    public class DeparmentService : BaseService, IDeparmentServices
    {
        private readonly IRepositoryAsync<Department> _Repo;
        private readonly IMapper _mapper;
        private readonly IValidator<DeparmentDto> _validator;
        public DeparmentService(IRepositoryAsync<Department> Repo, IMapper mapper, IValidator<DeparmentDto> validator, IHttpContextAccessor context) : base(context)
        {
            _Repo = Repo;
            _mapper = mapper;
            _validator = validator;
        }

        private async Task<bool> ExitsAsync(int Id)
        {
            var result = await _Repo.Exists(x => x.DeparmentId.Equals(Id));
            if (result) { return true; }
            throw new ApiException("Gender not found");
        }

        public async Task<Response<DeparmentVm>> InsertAsync(DeparmentDto dto)
        {

            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            var obj = _mapper.Map<Department>(dto);

            obj.IsActive = true;


            return new Response<DeparmentVm>(_mapper.Map<DeparmentVm>(await _Repo.AddAsync(obj)));

        }

        public async Task<Response<DeparmentVm>> UpdateAsync(int id, DeparmentDto dto)
        {
            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            await ExitsAsync(id);

            var objDb = await _Repo.WhereAsync(x => x.DeparmentId.Equals(id));
            var obj = _mapper.Map<Department>(dto);

            obj.DeparmentId = objDb.DeparmentId;



            return new Response<DeparmentVm>(_mapper.Map<DeparmentVm>(await _Repo.UpdateAsync(obj)));
        }

        public async Task<Response<DeparmentVm>> GetByIdAsync(int id)
        {
            var data = await _Repo.GetByIdAsync(id);
            if (data == null)
            {
                throw new KeyNotFoundException($"Gender not found by id={id}");
            }

            return new Response<DeparmentVm>(_mapper.Map<DeparmentVm>(data));
        }

        public async Task<Response<IList<DeparmentVm>>> GetAllAsync()
        {
            var list = await _Repo.ListAllAsync();
            if (list == null)
            {
                throw new KeyNotFoundException($"Gender not found");
            }

            return new Response<IList<DeparmentVm>>(_mapper.Map<IList<DeparmentVm>>(list));
        }

        public async Task<PagedResponse<IList<DeparmentVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null)
        {

            List<Expression<Func<Department, bool>>> queryFilter = new List<Expression<Func<Department, bool>>>();
            List<Expression<Func<Department, Object>>> includes = new List<Expression<Func<Department, Object>>>();




            //if (filter != null || filter.Length > 0)
            //{
            //    queryFilter.Add(x => x.Id();
            //}

            var list = await _Repo.GetPagedList(pageNumber, pageSize, queryFilter, includes: includes);
            if (list == null || list.Data.Count == 0)
            {
                throw new KeyNotFoundException($"Gender not found");
            }

            return new PagedResponse<IList<DeparmentVm>>(_mapper.Map<IList<DeparmentVm>>(list.Data), list.PageNumber, list.PageSize, list.TotalCount);
        }


    }
}
