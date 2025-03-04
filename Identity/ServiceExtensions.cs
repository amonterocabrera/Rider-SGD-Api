using SGDPEDIDOS.application.DTOs.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SGDPEDIDOS.application.Interfaces.Services.Security;
using SGDPEDIDOS.Application.Wrappers;
using SGDPEDIDOS.Identity.Services;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Identity
{
    public static class ServiceExtensions
    {
        public static void AddIdentityInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddSingleton(JWTSettings => configuration.GetSection("JWTSettings").Get<JWTSettings>());

            #region Services
            services.AddScoped<ICryptographyProcessorService, CryptographyProcessorService>();
            services.AddScoped<IAccountService, AccountService>(); 
            #endregion
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                };

                o.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies[configuration["JWTSettings:CookieName"]];
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Response<string>("No autorizado"));
                        return context.Response.WriteAsync(result);

                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Response<string>("No autorizado, sin permisos"));
                        return context.Response.WriteAsync(result);
                    }
                };
            });



            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }
    }
}
