using Microsoft.EntityFrameworkCore;
using SGDPEDIDOS.Api.Models;
using SGDPEDIDOS.Domain.Entities;
using SGDPEDIDOS.Domain.Entities.StoreProcedure;
using SGDPEDIDOS.Domain.Entities.View;

using SGDPEDIDOS.Infrastructure.Data.Configuration;

namespace SGDPEDIDOS.Infrastructure.Data
{
    public partial class PrincipalContext : DbContext
    {
        public PrincipalContext()
        {
        }

        public PrincipalContext(string CnString) : base(GetOptions(CnString))
        {
        }

        public static DbContextOptions GetOptions(string CnString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), CnString).Options;
        }

        public PrincipalContext(DbContextOptions<PrincipalContext> options)
            : base(options)
        {
        }


        public virtual DbSet<Company> Companies { get; set; }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Favorite> Favorites { get; set; }

        public virtual DbSet<Gender> Genders { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<InvoicesDetail> InvoicesDetails { get; set; }

        public virtual DbSet<LogsCreditsBalance> LogsCreditsBalances { get; set; }

        public virtual DbSet<LogsCreditsUser> LogsCreditsUsers { get; set; }

        public virtual DbSet<Menu> Menus { get; set; }

        public virtual DbSet<OrdersStatus> OrdersStatuses { get; set; }

        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

        public virtual DbSet<RecoverKey> RecoverKeys { get; set; }

        public virtual DbSet<Suggestion> Suggestions { get; set; }

        public virtual DbSet<TypeSuggestion> TypeSuggestions { get; set; }

        public virtual DbSet<TypesCompany> TypesCompanies { get; set; }

        public virtual DbSet<TypesIdentification> TypesIdentifications { get; set; }

        public virtual DbSet<TypesMenu> TypesMenus { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UsersType> UsersTypes { get; set; }

        public virtual DbSet<WeeklyDay> WeeklyDays { get; set; }

        public virtual DbSet<WeeklyMenu> WeeklyMenus { get; set; }

        public virtual DbSet<WeeklyMenusDetail> WeeklyMenusDetails { get; set; }

        public virtual DbSet<VReport> VReports { get; set; }
        public virtual DbSet<SupplierCompany> SupplierCompany { get; set; }

        public virtual DbSet<Image> Images { get; set; }

        public virtual DbSet<sp_week_menuResult> sp_week_menuResult { get; set; }

        public virtual DbSet<sp_view_week_menu_supplierResult> sp_view_week_menu_supplierResult { get; set; }

        public virtual DbSet<VReportSuggestion> VReportSuggestions { get; set; }

        public virtual DbSet<VReportSupplier> VReportSuppliers { get; set; }
        public virtual DbSet<VReportSupplierGroup> VReportSupplierGroups { get; set; }
        public virtual DbSet<VReportUserConsumedClient> VReportUserConsumedClients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelsConfiguration.Configuration(modelBuilder); 
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}