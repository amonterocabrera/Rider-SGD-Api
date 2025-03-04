using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SGDPEDIDOS.application.Interfaces.Services.Security;
using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel.Recoverykey;
using SGDPEDIDOS.Application.DTOs.ViewModel.Utils;
using SGDPEDIDOS.Application.Exceptions;
using SGDPEDIDOS.Application.Interfaces.Repositories;
using SGDPEDIDOS.Application.Interfaces.Services;
using SGDPEDIDOS.Application.Wrappers;
using SGDPEDIDOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Infrastructure.Services
{
    public class RecoverKeyService : BaseService, IRecoverKeyService
    {
        private readonly IRepositoryAsync<RecoverKey> _RecuperarClavesRepo;
        private readonly ICryptographyProcessorService _cryptographyProcessorService;
        private readonly IMapper _mapper;
        private readonly IValidator<RecoverKeyDto> _validator;
        private readonly IRepositoryAsync<User> _UsuarioRepo;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;


        public RecoverKeyService(IRepositoryAsync<RecoverKey> RecuperarClavesRepo, IConfiguration config, ICryptographyProcessorService cryptographyProcessorService, IEmailService emailService, IRepositoryAsync<User> usuarioRepo, IMapper mapper, IValidator<RecoverKeyDto> validator, IHttpContextAccessor context) : base(context)
        {
            _RecuperarClavesRepo = RecuperarClavesRepo;
            _mapper = mapper;
            _validator = validator;
            _UsuarioRepo = usuarioRepo;
            _config = config;
            _emailService = emailService;
            _cryptographyProcessorService = cryptographyProcessorService;
        }

        private async Task<bool> ExitsAsync(int Id)
        {
            var result = await _RecuperarClavesRepo.Exists(x => x.RecoveryId.Equals(Id));
            if (result) { return true; }
            throw new ApiException("RecuparClave no Encontrado");
        }

        public async Task<Response<string>> InsertAsync(RecoverKeyViewModel dto)
        {
            var result = await _UsuarioRepo.WhereAllAsync(x => x.Email == dto.Email);
            if (result is null)
            {
                throw new KeyNotFoundException($"Este Email no está registrado");
            }
            if (result.Count == 0)
            {
                throw new KeyNotFoundException($"Este Email no está registrado");
            }
            var usuario = result.LastOrDefault();
            //var valResult = _validator.Validate(dto);
            //if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            await VencerEnlacesAnteriores(usuario.UserId);

            var recuperarModel = new RecoverKeyUtils().GenerarModelo(usuario.UserId);

            //var obj = _mapper.Map<RecuperarClave>(dto);

            var respuesta = await _RecuperarClavesRepo.AddAsync(recuperarModel);

            _emailService.Send(usuario.Email, "SGD-Pedidos Recuperación de contraseña", "pruebadatos", true, usuario.FirstName , "datos", "RecoverKey.txt", GenerarEnlace(recuperarModel.SecurityKey));
            return new Response<string>("Te hemos enviado un e-mail para confirmar el cambio de contraseña");

        }
        private async Task<bool> VencerEnlacesAnteriores(int Id_usuario)
        {
            try
            {
                var result = await _RecuperarClavesRepo.WhereAllAsync(rc => rc.UserId == Id_usuario && rc.Processed == false && rc.Expired == false);
                if (result != null)
                {

                    foreach (var _item in result)
                    {
                        _item.Expired = true;
                      await   _RecuperarClavesRepo.UpdateAsync(_item);  
                      
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string GenerarEnlace(string key)
        {
            var _rutaNuevoPassword = _config.GetValue<string>("LinkNuevoPassword");
            return $"<a href='{_rutaNuevoPassword}{key}' style='padding:8px 12px;text-decoration:none;display:block;color:#fff;background-color:#002751;border-radius:4px;width:auto; width:calc(70% - 2px);font-family:Arial, Helvetica Neue, Helvetica, sans-serif;text-align:center;mso-border-alt:none;word-break:keep-all;' target='_blank'><span style='font-size: 14px;font-weight:500;line-height: 2 word-break: break-word; mso-line-height-alt:32px;'>Recuperar contraseña</span></span></a>";
        }

        public async Task<int> VerificarKey(RecoverKeyViewModelurl model)
        {
            try
            {
                // SE BUSCA EN LA BASE DE DATOS ALGUN REGISTRO QUE HAGA MATCH 
                var resultado = await _RecuperarClavesRepo.WhereAllAsync(rc => rc.SecurityKey ==  model.Key);
                var recupera = resultado.LastOrDefault();

                // SI NO EXISTE NINGUN REGISTRO
                if (resultado == null)
                {
                    return 404;
                }

                if (recupera.Processed == true)
                {
                    return 201;
                }

                if (recupera.Expired == true)
                {
                    return 401;
                }

                TimeSpan hoy = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0);
                var horaActual = (int)hoy.TotalSeconds;

                if (recupera.KeyExpirationDate < horaActual)
                {
                    // ENLACE EXPIRADO
                    return 401;
                }
                else
                {
                    // ENLACE AUN NO HA EXPIRADO
                    return 200;
                }
            }
            catch (Exception)
            {
                return 500;
            }
        }   
     
        public async Task<Response<RecoverKeyVm>> Actualizar(RecoverKeyUpdate model)
        {
            try
            {
                // SE VERIFICA SI EL ENLACE AUN ES VALIDO 
                //if (await VerificarKey(model.Key) == 200)
                //{
                // SE GENERAN LAS NUEVAS CREDENCIALES
                var password = _cryptographyProcessorService.GetPasswordAndSecurityKeyInfo(model.Contraseña);

                // SE BUSCA EL ID DEL USUARIO
                var registry = await _RecuperarClavesRepo.WhereAsync(rc => rc.SecurityKey == model.Key && rc.Processed == false && rc.Expired == false);

                    // SE BUSCA EL USUARIO POR EL ID
                var userAccount = await _UsuarioRepo.WhereAsync(u => u.UserId == registry.UserId);

            // SE MODIFICA EL REGISTRO EN BASE DE DATOS    
                userAccount.Password = password.HashedPassword;
                userAccount.UserSecurityKey = password.SecurityKey;

                registry.Processed = true;
                //await _RecuperarClavesRepo.UpdateAsync(registry);
                await _UsuarioRepo.UpdateAsync(userAccount);
                    //var obj = _mapper.Map<RecuperarClave>(model);

                return new Response<RecoverKeyVm>(_mapper.Map<RecoverKeyVm>(await _RecuperarClavesRepo.UpdateAsync(registry)));

            }
            catch (Exception ex)
            {
                return  new Response<RecoverKeyVm>("Ocurrio error al guardar el registro");
            }
        }
        public async Task<Response<RecoverKeyVm>> GetByIdAsync(int id)
        {
            var data = await _RecuperarClavesRepo.GetByIdAsync(id);
            if (data == null)
            {
                throw new KeyNotFoundException($"RecuperarClave no encontrado con id={id}");
            }

            return new Response<RecoverKeyVm>(_mapper.Map<RecoverKeyVm>(data));
        }
        public async Task<PagedResponse<IList<RecoverKeyVm>>> GetPagedListAsync(int pageNumber, int pageSize, string filter = null)
        {

            List<Expression<Func<RecoverKey, bool>>> queryFilter = new List<Expression<Func<RecoverKey, bool>>>();
            List<Expression<Func<RecoverKey, Object>>> includes = new List<Expression<Func<RecoverKey, Object>>>();

            var list = await _RecuperarClavesRepo.GetPagedList(pageNumber, pageSize, queryFilter, includes: includes);
            if (list == null || list.Data.Count == 0)
            {
                throw new KeyNotFoundException($"RecuperarClave no encontrado");
            }

            return new PagedResponse<IList<RecoverKeyVm>>(_mapper.Map<IList<RecoverKeyVm>>(list.Data), list.PageNumber, list.PageSize, list.TotalCount);
        }


       

    }
}
