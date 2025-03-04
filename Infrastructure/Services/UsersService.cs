using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SGDPEDIDOS.application.Interfaces.Services.Security;
using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.Exceptions;
using SGDPEDIDOS.Application.Interfaces.Repositories;
using SGDPEDIDOS.Application.Interfaces.Services;
using SGDPEDIDOS.Application.Wrappers;
using SGDPEDIDOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace SGDPEDIDOS.Infrastructure.Services
{
    public class UsersService : BaseService, IUsersService
    {
        private readonly IRepositoryAsync<User> _UsuarioRepo;
        private readonly ICryptographyProcessorService _cryptographyProcessorService;
        protected readonly DbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IValidator<UsersDto> _validator;
        private readonly IValidator<UsersDtoList> _validatorList;
        private readonly IValidator<UsersImagenDto> _uservalidator;
        private readonly IEmailService _emailService;
        private readonly IAzureStorage _azureStorage;
        public UsersService(ICryptographyProcessorService cryptographyProcessorService, IAzureStorage azureStorage, IValidator<UsersImagenDto> uservalidator, IValidator<UsersDtoList> validatorList, DbContext dbContext, IEmailService emailService, IRepositoryAsync<User> usuarioRepo, IMapper mapper, IValidator<UsersDto> validator, IHttpContextAccessor context) : base(context)
        {
            _UsuarioRepo = usuarioRepo;
            _dbContext = dbContext;
            _mapper = mapper;
            _validator = validator;
            _uservalidator = uservalidator;
            _cryptographyProcessorService = cryptographyProcessorService;
            _emailService = emailService;
            _azureStorage = azureStorage;
            _validatorList = validatorList;

        }

        private async Task<bool> ExitsAsync(int Id)
        {
            var result = await _UsuarioRepo.Exists(x => x.UserId.Equals(Id));
            if (result) { return true; }
            throw new ApiException("Usuarios no encontrada");
        }
        public async Task<Response<UsersVm>> RegisterAsync(UsersDto dto)
        {
            var result = await _UsuarioRepo.Exists(x => x.Email.Equals(dto.Email));
            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            if (result == true)
            {
                throw new KeyNotFoundException($"Usuario ya Existe ={dto.Email}");
            }
            var obj = _mapper.Map<User>(dto);
            var password = _cryptographyProcessorService.GetPasswordAndSecurityKeyInfo(dto.Password);
            obj.Password = password.HashedPassword;
            obj.UserSecurityKey = password.SecurityKey;
            obj.CreationDate = DateTime.Now;
            obj.IsActive = true;
            return new Response<UsersVm>(_mapper.Map<UsersVm>(await _UsuarioRepo.AddAsync(obj)));

        }



        public async Task<Response<List<UsersVm>>> InsertListAsync(List<UsersDtoList> userListDto)
        {
            int companyId = base.GetLoggerUserOrganizationId();

            var validationResults = userListDto
                .Select(dto => new { Dto = dto, ValidationResult = _validatorList.Validate(dto) })
                .ToList();


            var validUsers = validationResults.Where(vr => vr.ValidationResult.IsValid).Select(vr => vr.Dto).ToList();
            var invalidUsers = validationResults
                .Where(vr => !vr.ValidationResult.IsValid)
                .Select(vr => new { vr.Dto.Email, vr.ValidationResult.Errors })
                .ToList();


            var emails = validUsers.Select(dto => dto.Email).ToList();


            var existingUsers = await _UsuarioRepo.GelAllAsync(x => emails.Contains(x.Email));
            var existingEmails = new HashSet<string>(existingUsers.Select(x => x.Email));

            var usersToAdd = new List<User>();
            var failedUsers = new List<string>();

            foreach (var dto in validUsers)
            {
                if (existingEmails.Contains(dto.Email))
                {
                    failedUsers.Add($"Usuario ya Existe: {dto.Email}");
                    continue;
                }

                try
                {
                    var user = _mapper.Map<User>(dto);
                    var passwordInfo = _cryptographyProcessorService.GetPasswordAndSecurityKeyInfo(dto.UserIdentification);
                    user.Password = passwordInfo.HashedPassword;
                    user.UserSecurityKey = passwordInfo.SecurityKey;
                    user.CreationDate = DateTime.Now;
                    user.IsAdmin = false;
                    user.IsActive = true;
                    user.CompanyId = companyId;
                    user.UserTypeId = 3;//Colaborador
                    user.TypeIdentificationId = 2;//Cédula
                    user.GenderId = 4;
                    user.PayrollDiscount = dto.PayrollDiscount;
                    user.LimiteCreditPayroll = dto.PayrollDiscount;
                    user.CreditAvailable = dto.CreditAvailable;
                    user.PhoneNumber = dto.CellPhoneNumber;
                    user.DeparmentId = 1;

                    await _UsuarioRepo.AddAsync(user);
                }
                catch (Exception ex)
                {
                    failedUsers.Add($"Error al procesar usuario {dto.Email}: {ex.Message}");
                }
            }

       

            var insertedUsersVm = _mapper.Map<List<UsersVm>>(usersToAdd);


            return new Response<List<UsersVm>>(_mapper.Map<List<UsersVm>>(failedUsers));
        }


        public async Task<Response<List<UsersVm>>> UpdateBalanceListAsync(List<CreditAvailableDtoList> userListDto)
        {
            int companyId = base.GetLoggerUserOrganizationId();
            List<UsersVm> listUser = new List<UsersVm>();

            foreach (var dto in userListDto)
            {
                var objDb = await _UsuarioRepo.WhereAsync(x => x.UserId.Equals(dto.UserId));
                try
                {
                    objDb.CreditAvailable = dto.CreditAvailable;
                    objDb.PayrollDiscount = dto.PayrollDiscount;
                    objDb.LimiteCreditPayroll = dto.LimiteCreditPayroll;
                    await _UsuarioRepo.UpdateAsync(objDb);

                    listUser.Add(_mapper.Map<UsersVm>(objDb));

                }
                catch (Exception)
                {

                    throw;
                }

            }



            return new Response<List<UsersVm>>(listUser);
        }




        public async Task<Response<UsersVm>> InsertAsync(UsersDto dto)
        {

            var result = await _UsuarioRepo.Exists(x => x.Email.Equals(dto.Email));
            var ValidationNickname = await _UsuarioRepo.Exists(x => x.Email.Equals(dto.Email));
            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            if (result == true)
            {
                throw new KeyNotFoundException($"Usuario ya Existe ={dto.Email}");
            }
            if (ValidationNickname == true)
            {
                throw new KeyNotFoundException($"Nombre de Usuario ya Existe ={dto.Email}");
            }
            var obj = _mapper.Map<User>(dto);


            var password = _cryptographyProcessorService.GetPasswordAndSecurityKeyInfo(dto.Password);
            obj.Password = password.HashedPassword;
            obj.UserSecurityKey = password.SecurityKey;
            obj.CreationDate = DateTime.Now;
            obj.IsAdmin = false;
            obj.IsActive = true;


            return new Response<UsersVm>(_mapper.Map<UsersVm>(await _UsuarioRepo.AddAsync(obj)));
        }

        public async Task<Response<UsersVm>> InsertImagenAsync(UsersImagenDto dto)
        {
            try
            {
                var result = await _UsuarioRepo.Exists(x => x.Email.Equals(dto.Email));
                var ValidationNickname = await _UsuarioRepo.Exists(x => x.Email.Equals(dto.Email));
                var valResult = _uservalidator.Validate(dto);
                if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

                if (result == true)
                {
                    string msj = $"Usuario ya Existe ={dto.Email}";


                    return new Response<UsersVm>(msj);

                }
                if (ValidationNickname == true)
                {
                    return new Response<UsersVm>();
                }

                var obj = _mapper.Map<User>(dto);


                var password = _cryptographyProcessorService.GetPasswordAndSecurityKeyInfo(dto.Password);
                obj.Password = password.HashedPassword;
                obj.UserSecurityKey = password.SecurityKey;
                obj.CreationDate = DateTime.Now;
                obj.IsAdmin = false;
                obj.IsActive = true;
                obj.LimiteCreditPayroll = dto.PayrollDiscount;

                obj = await _UsuarioRepo.AddAsync(obj);
                if (dto.Image != null)
                {
                    // Blob storage --- aqui inserta en blobstorage 
                    Guid ArchivoBLock = Guid.NewGuid();
                    var NombreArchivoBlock = ArchivoBLock.ToString().Substring(0, 8);
                    var stream = new MemoryStream(Convert.FromBase64String(dto.Image.ImageName));
                    var archivo = await _azureStorage.UploadAsync(stream, "." + dto.Image.ContentType);
                    NombreArchivoBlock = archivo.Blob.Name;

                    Domain.Entities.Image data = new Domain.Entities.Image()
                    {
                        UserId = obj.UserId,
                        CompanyId = obj.CompanyId,
                        CompanyCode = "amc",
                        ImageName = NombreArchivoBlock,
                        ContentType = dto.Image.ContentType,
                        IsActive = true,
                        CreationDate = DateTime.Now,
                        CreatedBy1 = base.GetLoggerUserId(),
                    };

                    await _dbContext.AddAsync(data);

                }

                await _dbContext.SaveChangesAsync();
                return new Response<UsersVm>(_mapper.Map<UsersVm>(obj));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Response<UsersVm>> GetByIdAsync(int id)
        {
            var data = await _UsuarioRepo.GetByIdAsync(id);
            if (data == null)
            {
                throw new KeyNotFoundException($"Usuarios not found by id={id}");
            }

            return new Response<UsersVm>(_mapper.Map<UsersVm>(data));
        }



        public async Task<Response<UsersVm>> UpdateAsync(int id, UsersDto dto)
        {
            try
            {
                var userId = base.GetLoggerUserId();
                await ExitsAsync(id);
                var objDb = await _UsuarioRepo.WhereAsync(x => x.UserId.Equals(id));
                var objDbCopy = JsonConvert.DeserializeObject<User>(
                                    JsonConvert.SerializeObject(objDb)
                                );

                if (objDb == null)
                {
                    throw new KeyNotFoundException("Usuario no encontrado.");
                }

                var mapper = _mapper.Map(dto, objDb);
                mapper.ModifiedBy = userId;
                mapper.ModifiedDate = DateTime.UtcNow;
                mapper.IsAdmin = objDbCopy.IsAdmin;
                mapper.CreationDate = objDbCopy.CreationDate;
                mapper.CreatedBy = objDbCopy.CreatedBy;
                mapper.LimiteCreditPayroll = objDbCopy.PayrollDiscount;
                mapper.Password = objDbCopy.Password;
                mapper.UserSecurityKey = objDbCopy.UserSecurityKey;
                mapper.LastAccess = objDbCopy.LastAccess;


                var result = _UsuarioRepo.UpdateAsync(mapper);
                return new Response<UsersVm>(_mapper.Map<UsersVm>(mapper));

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<Response<UsersVm>> UpdateSubsidyamount(int id, UpdateSubsidyAmountDto dto)
        {
            //var valResult = _validator.Validate(dto);
            //if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);
            await ExitsAsync(id);

            var objDb = await _UsuarioRepo.WhereAsync(x => x.UserId.Equals(id));
            var obj = _mapper.Map<User>(dto);

            obj.CompanyId = objDb.CompanyId;
            obj.UserId = objDb.UserId;
            obj.Email = objDb.Email;
            obj.Password = objDb.Password;
            obj.UserSecurityKey = objDb.UserSecurityKey;
            obj.CreationDate = objDb.CreationDate;
            obj.LastAccess = objDb.LastAccess;
            obj.AccountLocked = objDb.AccountLocked;
            obj.LoginAttempts = objDb.LoginAttempts;
            obj.MustResetPassword = objDb.MustResetPassword;
            obj.UserTypeId = objDb.UserTypeId;
            obj.TypeIdentificationId = objDb.TypeIdentificationId;
            obj.UserIdentification = objDb.UserIdentification;
            obj.CreditAvailable = objDb.CreditAvailable;
            obj.PayrollDiscount = objDb.PayrollDiscount;
            obj.LimiteCreditPayroll = objDb.LimiteCreditPayroll;
            return new Response<UsersVm>(_mapper.Map<UsersVm>(await _UsuarioRepo.UpdateAsync(obj)));
        }

        


        public async Task<Response<UsersVm>> UpdatePasswordAsync(int id, UpdatePasswordUsersDto dto)
        {
            //var valResult = _validator.Validate(dto);
            //if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);
            await ExitsAsync(id);

            var objDb = await _UsuarioRepo.WhereAsync(x => x.UserId.Equals(id));
            var obj = _mapper.Map<User>(dto);
            obj.UserId = objDb.UserId;
            obj.CompanyId = objDb.CompanyId;
            obj.FirstName = objDb.FirstName;
            obj.SecondName = objDb.SecondName;
            obj.FirstLastName = objDb.FirstLastName;
            obj.SecondLastName = objDb.SecondLastName;
            obj.Email = objDb.Email;
            obj.GenderId = objDb.GenderId;
            obj.BirthdayDate = objDb.BirthdayDate;
            obj.AccountLocked = objDb.AccountLocked;
            obj.LoginAttempts = objDb.LoginAttempts;
            obj.MustResetPassword = objDb.MustResetPassword;
            obj.UserTypeId = objDb.UserTypeId;
            obj.TypeIdentificationId = objDb.TypeIdentificationId;
            obj.TypeIdentification = objDb.TypeIdentification;
            obj.UserIdentification = objDb.UserIdentification;
            obj.CreditAvailable = objDb.CreditAvailable;
            obj.PayrollDiscount = objDb.PayrollDiscount;
            obj.LimiteCreditPayroll = objDb.LimiteCreditPayroll;
            obj.PhoneNumber = objDb.PhoneNumber;
            obj.CellPhoneNumber = objDb.CellPhoneNumber;
            obj.IsActive = objDb.IsActive;
            obj.CreationDate = objDb.CreationDate;
            obj.DeletedDate = objDb.DeletedDate;
            obj.CreatedBy = objDb.CreatedBy;
            obj.ModifiedDate = objDb.ModifiedDate;
            obj.ModifiedBy = objDb.ModifiedBy;

            obj.LastAccess = obj.LastAccess;


            var password = _cryptographyProcessorService.GetPasswordAndSecurityKeyInfo(dto.Password);
            obj.Password = password.HashedPassword;
            obj.UserSecurityKey = password.SecurityKey;

            return new Response<UsersVm>(_mapper.Map<UsersVm>(await _UsuarioRepo.UpdateAsync(obj)));

        }
        public async Task<PagedResponse<IList<UsersVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null)
        {

            List<Expression<Func<User, bool>>> queryFilter = new List<Expression<Func<User, bool>>>();
            List<Expression<Func<User, Object>>> includes = new List<Expression<Func<User, Object>>>();

            var organizationId = base.GetLoggerUserOrganizationId();

            queryFilter.Add(x => x.CompanyId.Equals(organizationId));

            if (filter != null || filter.Length > 0)
            {
                queryFilter.Add(x => x.Email.Contains(filter) || x.UserIdentification.Contains(filter) || x.FirstName.Contains(filter) || x.SecondName.Contains(filter) || x.FirstLastName.Contains(filter) || x.SecondLastName.Contains(filter));
            }


            var list = await _UsuarioRepo.GetPagedList(
                        pageNumber,
                        pageSize,
                        queryFilter,
                        orderBy: query => query.OrderByDescending(x => x.IsActive == true).OrderBy(u => u.FirstName).ThenBy(u => u.FirstLastName),
                        includes: includes
                    );
            
            return new PagedResponse<IList<UsersVm>>(_mapper.Map<IList<UsersVm>>(list.Data.OrderByDescending(d => d.IsActive).ToList()), list.PageNumber, list.PageSize, list.TotalCount);
        }

        public async Task<PagedResponse<IList<UsersVm>>> GetUserListAdmin(int pageNumber, int pageSize, int companyId, int userTypeId, string filter = null)
        {

            List<Expression<Func<User, bool>>> queryFilter = new List<Expression<Func<User, bool>>>();
            List<Expression<Func<User, Object>>> includes = new List<Expression<Func<User, Object>>>();


            queryFilter.Add(x => x.UserTypeId.Equals(userTypeId));
            if (companyId > 0)
            {
                queryFilter.Add(x => x.CompanyId.Equals(companyId));
            }



            if (!string.IsNullOrWhiteSpace(filter))
            {
                queryFilter.Add(x => x.Email.Contains(filter) || x.UserIdentification.Contains(filter) || x.FirstName.Contains(filter) || x.SecondName.Contains(filter) || x.FirstLastName.Contains(filter) || x.SecondLastName.Contains(filter));
            }


            var list = await _UsuarioRepo.GetPagedList(
                        pageNumber,
            pageSize,
            queryFilter,
                        orderBy: query => query.OrderByDescending(x => x.IsActive == true).OrderBy(u => u.FirstName).ThenBy(u => u.FirstLastName),
                        includes: includes
                    );
          
            return new PagedResponse<IList<UsersVm>>(_mapper.Map<IList<UsersVm>>(list.Data.OrderByDescending(d => d.IsActive).ToList()), list.PageNumber, list.PageSize, list.TotalCount);
        }



    }
}
