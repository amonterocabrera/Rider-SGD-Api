using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.Exceptions;
using SGDPEDIDOS.Application.Interfaces.Repositories;
using SGDPEDIDOS.Application.Interfaces.Services;
using SGDPEDIDOS.Application.Wrappers;
using SGDPEDIDOS.Domain.Entities;
using SGDPEDIDOS.Domain.Entities.StoreProcedure;
using SGDPEDIDOS.Infrastructure.Data;
using SGDPEDIDOS.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DSCALIDAD.Infrastructure.Services
{
    public class WeeklyMenusDetailService : BaseService, IWeeklyMenusDetailService
    {
        private readonly IRepositoryAsync<WeeklyMenusDetail> _WeeklyMenusDetailRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<WeeklyMenusDetailDto> _validator;
        private readonly PrincipalContext _dbcontext;
        public WeeklyMenusDetailService(IRepositoryAsync<WeeklyMenusDetail> WeeklyMenusDetailRepo, PrincipalContext dbcontext, IMapper mapper, IValidator<WeeklyMenusDetailDto> validator, IHttpContextAccessor context) : base(context)
        {
            _WeeklyMenusDetailRepo = WeeklyMenusDetailRepo;
            _dbcontext = dbcontext;
            _mapper = mapper;
            _validator = validator;
        }

        private async Task<bool> ExitsAsync(int Id)
        {
            var result = await _WeeklyMenusDetailRepo.Exists(x => x.WeeklyMenusDetailsId.Equals(Id));
            if (result) { return true; }
            throw new ApiException("WeeklyMenusDetail not found");
        }

        public async Task<Response<WeeklyMenusDetailVm>> InsertAsync(WeeklyMenusDetailDto dto)
        {

            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            var obj = _mapper.Map<WeeklyMenusDetail>(dto);


            return new Response<WeeklyMenusDetailVm>(_mapper.Map<WeeklyMenusDetailVm>(await _WeeklyMenusDetailRepo.AddAsync(obj)));

        }

        public async Task<Response<WeeklyMenusDetailVm>> UpdateAsync(int id, WeeklyMenusDetailDto dto)
        {
            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            await ExitsAsync(id);

            var objDb = await _WeeklyMenusDetailRepo.WhereAsync(x => x.WeeklyMenusDetailsId.Equals(id));
            var obj = _mapper.Map<WeeklyMenusDetail>(dto);

            obj.WeeklyMenusDetailsId = objDb.WeeklyMenusDetailsId;


            return new Response<WeeklyMenusDetailVm>(_mapper.Map<WeeklyMenusDetailVm>(await _WeeklyMenusDetailRepo.UpdateAsync(obj)));
        }

        public async Task<Response<WeeklyMenusDetailVm>> GetByIdAsync(int id)
        {
            var data = await _WeeklyMenusDetailRepo.GetByIdAsync(id);
            if (data == null)
            {
                throw new KeyNotFoundException($"WeeklyMenusDetail not found by id={id}");
            }

            return new Response<WeeklyMenusDetailVm>(_mapper.Map<WeeklyMenusDetailVm>(data));
        }



        public async Task<PagedResponse<IList<WeeklyMenusDetailVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null)
        {

            List<Expression<Func<WeeklyMenusDetail, bool>>> queryFilter = new List<Expression<Func<WeeklyMenusDetail, bool>>>();
            List<Expression<Func<WeeklyMenusDetail, Object>>> includes = new List<Expression<Func<WeeklyMenusDetail, Object>>>();




            //if (filter != null || filter.Length > 0)
            //{
            //    queryFilter.Add(x => x.Id();
            //}

            var list = await _WeeklyMenusDetailRepo.GetPagedList(pageNumber, pageSize, queryFilter, includes: includes);
            if (list == null || list.Data.Count == 0)
            {
                throw new KeyNotFoundException($"WeeklyMenusDetail not found");
            }

            return new PagedResponse<IList<WeeklyMenusDetailVm>>(_mapper.Map<IList<WeeklyMenusDetailVm>>(list.Data), list.PageNumber, list.PageSize, list.TotalCount);
        }
        public async Task<List<dynamic>> sp_week_menuAsync(int? weekly_menus_id)
        {
            int? company_id = base.GetLoggerUserOrganizationId();

            try
            {
                var sqlParameters = new[]
                {
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "company_id",
                        Value = company_id ?? (object)DBNull.Value,
                        SqlDbType = System.Data.SqlDbType.Int,
                    },

                     new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "weekly_menus_id",
                        Value = weekly_menus_id ?? (object)DBNull.Value,
                        SqlDbType = System.Data.SqlDbType.Int,
                    },
                };

                // Ejecutar el procedimiento almacenado y obtener resultados
                var dbResults = await _dbcontext.sp_week_menuResult
                    .FromSqlRaw("EXEC dbo.sp_week_menu @company_id,@weekly_menus_id", sqlParameters)
                    .ToListAsync();

                List<dynamic> finalResult = new List<dynamic>();

                foreach (var result in dbResults)
                {
                    string jsonString = result.Result; // Assumed property name is 'Result' containing JSON string
                    if (!string.IsNullOrEmpty(jsonString))
                    {
                        var deserializedResults = JsonConvert.DeserializeObject<List<dynamic>>(jsonString);
                        finalResult.AddRange(deserializedResults);
                    }
                }

                return finalResult;
            }
            catch (Exception ex)
            {
                // Manejar excepciones, registrar errores y devolver la respuesta adecuada
                throw new InvalidOperationException("Error ejecutando el procedimiento almacenado", ex);
            }
        }

        public async Task<List<dynamic>> getWeekAllBeforeMenus(int weekly_menus_id)
        {
            int? company_id = base.GetLoggerUserOrganizationId();

            try
            {
                var sqlParameters = new[]
                {
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "company_id",
                        Value = company_id ?? (object)DBNull.Value,
                        SqlDbType = System.Data.SqlDbType.Int,
                    },

                     new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "weekly_menus_id",
                        Value = weekly_menus_id,
                        SqlDbType = System.Data.SqlDbType.Int,
                    },
                };

                // Ejecutar el procedimiento almacenado y obtener resultados
                var dbResults = await _dbcontext.sp_week_menuResult
                    .FromSqlRaw("EXEC dbo.sp_week_menuAllAbove  @company_id,@weekly_menus_id", sqlParameters)
                    .ToListAsync();

                List<dynamic> finalResult = new List<dynamic>();

                foreach (var result in dbResults)
                {
                    string jsonString = result.Result; // Assumed property name is 'Result' containing JSON string
                    if (!string.IsNullOrEmpty(jsonString))
                    {
                        var deserializedResults = JsonConvert.DeserializeObject<List<dynamic>>(jsonString);
                        finalResult.AddRange(deserializedResults);
                    }
                }

                return finalResult;
            }
            catch (Exception ex)
            {
                // Manejar excepciones, registrar errores y devolver la respuesta adecuada
                throw new InvalidOperationException("Error ejecutando el procedimiento almacenado", ex);
            }
        }



        public async Task<dynamic> sp_view_week_menu_supplierAsync(int? weekly_menus_id, int? weekly_day)
        {
            int? company_id = base.GetLoggerUserOrganizationId();

            try
            {
                var sqlParameters = new[]
                {
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "company_id",
                        Value = company_id ?? (object)DBNull.Value,
                        SqlDbType = System.Data.SqlDbType.Int,
                    },
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "weekly_menus_id",
                        Value = weekly_menus_id ?? (object)DBNull.Value,
                        SqlDbType = System.Data.SqlDbType.Int,
                    },
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "weekly_day",
                        Value = weekly_day ?? (object)DBNull.Value,
                        SqlDbType = System.Data.SqlDbType.Int,
                    }
                };

               
                var dbResults = await _dbcontext.sp_view_week_menu_supplierResult
                    .FromSqlRaw("EXEC dbo.sp_view_week_menu_supplier @company_id, @weekly_menus_id, @weekly_day", sqlParameters)
                    .ToListAsync();

                List<dynamic> finalResult = new List<dynamic>();

                foreach (var result in dbResults)
                {
                    string jsonString = result.RESULTADO; 
                    if (!string.IsNullOrEmpty(jsonString))
                    {
                       
                        var deserializedResult = JsonConvert.DeserializeObject<dynamic>(jsonString);

                      
                        if (deserializedResult?.result != null)
                        {
                            foreach (var item in deserializedResult.result)
                            {
                                finalResult.Add(item);
                            }
                        }
                    }
                }

                return finalResult;
            }
            catch (Exception ex)
            {
                
                throw new InvalidOperationException("Error ejecutando el procedimiento almacenado", ex);
            }
        }


        public virtual async Task<int> sp_insert_delete_week_menu_supplierAsync(int? weekly_menus_id, int? weekly_day, int? menu_id, bool? is_active)
        {
            int? company_id = base.GetLoggerUserOrganizationId();
         
            var sqlParameters = new[]
                {
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "company_id",
                        Value = company_id ?? (object)DBNull.Value,
                        SqlDbType = System.Data.SqlDbType.Int,
                    },
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "weekly_menus_id",
                        Value = weekly_menus_id ?? (object)DBNull.Value,
                        SqlDbType = System.Data.SqlDbType.Int,
                    },
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "weekly_day",
                        Value = weekly_day ?? (object)DBNull.Value,
                        SqlDbType = System.Data.SqlDbType.Int,
                    },
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "menu_id",
                        Value = menu_id ?? (object)DBNull.Value,
                        SqlDbType = System.Data.SqlDbType.Int,
                    },
                    new Microsoft.Data.SqlClient.SqlParameter
                    {
                        ParameterName = "is_active",
                        Value = is_active ?? (object)DBNull.Value,
                        SqlDbType = System.Data.SqlDbType.Bit,
                    }
                };


            try
            {
               
                var result = await _dbcontext.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.sp_insert_delete_week_menu_supplier @company_id, @weekly_menus_id, @weekly_day, @menu_id, @is_active",
                    sqlParameters
               
                );



                return result;
            }
            catch (Exception ex)
            {
               
                throw new Exception("An error occurred while executing the stored procedure.", ex);
            }
        }


        public class Menu
        {
            public int MenuId { get; set; }
            public string MenuName { get; set; }
            public string MenuDescription { get; set; }
            public double MenuAmount { get; set; }
            public string PictureOne { get; set; }
            public int IsActive { get; set; }
        }






    }
}
