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
    public class UsersTypesService : BaseService, IUsersTypesService
    {
        private readonly IRepositoryAsync<UsersType> _UsersTypeRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<UsersTypesDto> _validator;
        public UsersTypesService(IRepositoryAsync<UsersType> UsersTypeRepo, IMapper mapper, IValidator<UsersTypesDto> validator, IHttpContextAccessor context) : base(context)
        {
            _UsersTypeRepo = UsersTypeRepo;
            _mapper = mapper;
            _validator = validator;
        }

        private async Task<bool> ExitsAsync(int Id)
        {
            var result = await _UsersTypeRepo.Exists(x => x.UserTypeId.Equals(Id));
            if (result) { return true; }
            throw new ApiException("Gender not found");
        }

        public async Task<Response<UsersTypesVm>> InsertAsync(UsersTypesDto dto)
        {

            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            var obj = _mapper.Map<UsersType>(dto);

            obj.IsActive = true;


            return new Response<UsersTypesVm>(_mapper.Map<UsersTypesVm>(await _UsersTypeRepo.AddAsync(obj)));

        }

        public async Task<Response<UsersTypesVm>> UpdateAsync(int id, UsersTypesDto dto)
        {
            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            await ExitsAsync(id);

            var objDb = await _UsersTypeRepo.WhereAsync(x => x.UserTypeId.Equals(id));
            var obj = _mapper.Map<UsersType>(dto);

            obj.UserTypeId = objDb.UserTypeId;



            return new Response<UsersTypesVm>(_mapper.Map<UsersTypesVm>(await _UsersTypeRepo.UpdateAsync(obj)));
        }

        public async Task<Response<UsersTypesVm>> GetByIdAsync(int id)
        {
            var data = await _UsersTypeRepo.GetByIdAsync(id);
            if (data == null)
            {
                throw new KeyNotFoundException($"Gender not found by id={id}");
            }

            return new Response<UsersTypesVm>(_mapper.Map<UsersTypesVm>(data));
        }

        public async Task<Response<IList<UsersTypesVm>>> GetAllAsync()
        {
            var list = await _UsersTypeRepo.ListAllAsync();
            if (list == null)
            {
                throw new KeyNotFoundException($"Gender not found");
            }

            return new Response<IList<UsersTypesVm>>(_mapper.Map<IList<UsersTypesVm>>(list));
        }

        public async Task<PagedResponse<IList<UsersTypesVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null)
        {

            List<Expression<Func<UsersType, bool>>> queryFilter = new List<Expression<Func<UsersType, bool>>>();
            List<Expression<Func<UsersType, Object>>> includes = new List<Expression<Func<UsersType, Object>>>();

            //if (filter != null || filter.Length > 0)
            //{
            //    queryFilter.Add(x => x.Id();
            //}

            var list = await _UsersTypeRepo.GetPagedList(pageNumber, pageSize, queryFilter, includes: includes);
            if (list == null || list.Data.Count == 0)
            {
                throw new KeyNotFoundException($"Gender not found");
            }

            return new PagedResponse<IList<UsersTypesVm>>(_mapper.Map<IList<UsersTypesVm>>(list.Data), list.PageNumber, list.PageSize, list.TotalCount);
        }


    }
}
