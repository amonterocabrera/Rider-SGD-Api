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
    public class TypesIdentificationsService : BaseService, ITypesIdentificationService
    {
        private readonly IRepositoryAsync<TypesIdentification> _TypesIdentificationRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<TypesIdentificationsDto> _validator;
        public TypesIdentificationsService(IRepositoryAsync<TypesIdentification> TypesIdentificationRepo, IMapper mapper, IValidator<TypesIdentificationsDto> validator, IHttpContextAccessor context) : base(context)
        {
            _TypesIdentificationRepo = TypesIdentificationRepo;
            _mapper = mapper;
            _validator = validator;
        }

        private async Task<bool> ExitsAsync(int Id)
        {
            var result = await _TypesIdentificationRepo.Exists(x => x.TypeIdentificationId.Equals(Id));
            if (result) { return true; }
            throw new ApiException("TypesIdentification not found");
        }

        public async Task<Response<TypesIdentificationsVm>> InsertAsync(TypesIdentificationsDto dto)
        {

            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            var obj = _mapper.Map<TypesIdentification>(dto);

            obj.IsActive = true;


            return new Response<TypesIdentificationsVm>(_mapper.Map<TypesIdentificationsVm>(await _TypesIdentificationRepo.AddAsync(obj)));

        }

        public async Task<Response<TypesIdentificationsVm>> UpdateAsync(int id, TypesIdentificationsDto dto)
        {
            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            await ExitsAsync(id);

            var objDb = await _TypesIdentificationRepo.WhereAsync(x => x.TypeIdentificationId.Equals(id));
            var obj = _mapper.Map<TypesIdentification>(dto);

            obj.TypeIdentificationId = objDb.TypeIdentificationId;



            return new Response<TypesIdentificationsVm>(_mapper.Map<TypesIdentificationsVm>(await _TypesIdentificationRepo.UpdateAsync(obj)));
        }

        public async Task<Response<TypesIdentificationsVm>> GetByIdAsync(int id)
        {
            var data = await _TypesIdentificationRepo.GetByIdAsync(id);
            if (data == null)
            {
                throw new KeyNotFoundException($"TypesIdentification not found by id={id}");
            }

            return new Response<TypesIdentificationsVm>(_mapper.Map<TypesIdentificationsVm>(data));
        }

        public async Task<Response<IList<TypesIdentificationsVm>>> GetAllAsync()
        {
            var list = await _TypesIdentificationRepo.ListAllAsync();
            if (list == null)
            {
                throw new KeyNotFoundException($"TypesIdentification not found");
            }

            return new Response<IList<TypesIdentificationsVm>>(_mapper.Map<IList<TypesIdentificationsVm>>(list));
        }

        public async Task<PagedResponse<IList<TypesIdentificationsVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null)
        {

            List<Expression<Func<TypesIdentification, bool>>> queryFilter = new List<Expression<Func<TypesIdentification, bool>>>();
            List<Expression<Func<TypesIdentification, Object>>> includes = new List<Expression<Func<TypesIdentification, Object>>>();




            //if (filter != null || filter.Length > 0)
            //{
            //    queryFilter.Add(x => x.Id();
            //}

            var list = await _TypesIdentificationRepo.GetPagedList(pageNumber, pageSize, queryFilter, includes: includes);
            if (list == null || list.Data.Count == 0)
            {
                throw new KeyNotFoundException($"TypesIdentification not found");
            }

            return new PagedResponse<IList<TypesIdentificationsVm>>(_mapper.Map<IList<TypesIdentificationsVm>>(list.Data), list.PageNumber, list.PageSize, list.TotalCount);
        }


    }
}
