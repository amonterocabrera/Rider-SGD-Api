using DSCALIDAD.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGDPEDIDOS.Application.Interfaces.Repositories;
using SGDPEDIDOS.Application.Interfaces.Services;
using SGDPEDIDOS.Domain.Entities;
using SGDPEDIDOS.Infrastructure.Data;
using SGDPEDIDOS.Infrastructure.Repositories;
using SGDPEDIDOS.Infrastructure.Services;
using System.Data;
using System.Data.SqlClient;

namespace SGDPEDIDOS.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddPersistenceInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PrincipalContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("default"),
                b => b.MigrationsAssembly(typeof(PrincipalContext).Assembly.FullName)));

            services.AddScoped<DbContext, PrincipalContext>();

            //SI necesito usar dapper o otro dataAccess solo es quitar este comentario//
            services.AddScoped<IDbConnection>(db =>
              new SqlConnection(configuration.GetConnectionString("default")));


            #region Repositories
            services.AddScoped(typeof(IRepositoryAsync<>), typeof(EFRepository<>));
            
            #endregion

            #region Services  
 
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IMenusService, MenusService>();
            services.AddScoped<ISuggestionsService, SuggestionsService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IRecoverKeyService, RecoverKeyService>();
            services.AddScoped<IWeeklyMenusDetailService, WeeklyMenusDetailService>();
            services.AddScoped<IWeeklyMenusService, WeeklyMenusService>();
            services.AddScoped<IWeeklyDaysService, WeeklyDaysService>();
            services.AddScoped<IFavoritesService, FavoritesService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ITypeSuggestionsService, TypeSuggestionsService>();
            services.AddScoped<ISupplierCompany, SupplierCompanyService >();
            services.AddScoped<ISupplierServices, SupplierService>();
            services.AddScoped<IGenderService, GendersService>();
            services.AddScoped<IUsersTypesService, UsersTypesService>();
            services.AddScoped<ITypesIdentificationService, TypesIdentificationsService>();
            services.AddScoped<IAzureStorage, AzureStorage>();
            services.AddScoped<IDeparmentServices, DeparmentService>();
            #endregion
        }
    }
}
 