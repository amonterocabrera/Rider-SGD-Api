using Microsoft.EntityFrameworkCore;
using SGDPEDIDOS.Api.Models;
using SGDPEDIDOS.Domain.Entities;
using SGDPEDIDOS.Domain.Entities.StoreProcedure;
using SGDPEDIDOS.Domain.Entities.View;

namespace SGDPEDIDOS.Infrastructure.Data.Configuration
{
    public class ModelsConfiguration
    {

        public static void Configuration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VReportUserConsumedClient>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("v_report_user_consumed_client");

                entity.Property(e => e.ClientAddress)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("client_address");
                entity.Property(e => e.ClientBrandName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("client_brand_name");
                entity.Property(e => e.ClientEmail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("client_email");
                entity.Property(e => e.ClientEmpresa)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("client_empresa");
                entity.Property(e => e.ClientId).HasColumnName("client_id");
                entity.Property(e => e.ClientIdentification)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("client_identification");
                entity.Property(e => e.ClientNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("client_number");
                entity.Property(e => e.ClientTypeId).HasColumnName("client_type_id");
                entity.Property(e => e.Deduction)
                    .HasColumnType("money")
                    .HasColumnName("deduction");
                entity.Property(e => e.EmailUser)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email_user");
                entity.Property(e => e.FirstLastName)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("first_last_name");
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("first_name");
                entity.Property(e => e.SecondLastName)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("second_last_name");
                entity.Property(e => e.SecondName)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("second_name");
                entity.Property(e => e.Total)
                    .HasColumnType("money")
                    .HasColumnName("total");
                entity.Property(e => e.UserId).HasColumnName("user_id");
            });
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DeparmentId).HasName("PK_Deparments");

                entity.Property(e => e.DeparmentId).HasColumnName("deparment_id");
                entity.Property(e => e.DeparmentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("deparment_name");
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))")
                    .HasColumnName("is_active");
            });
            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("companies");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.BrandName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("brand_name");

                entity.Property(e => e.City)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.CompanyIdentification)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("company_identification");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("company_name");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_date");

                entity.Property(e => e.StartServices)
                  .HasColumnType("time")
                  .HasColumnName("startServices");
                entity.Property(e => e.EndServices)
                  .HasColumnType("time")
                  .HasColumnName("endServices");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstAddress)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("first_address");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.MainContact)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("main_contact");

                entity.Property(e => e.MainContactIdentification)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("main_contact_identification");

                entity.Property(e => e.MainContactPhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("main_contact_phone_number");

                entity.Property(e => e.MainContactTypeIdentificationId).HasColumnName("main_contact_type_identification_id");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phone_number");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("postal_code");

                entity.Property(e => e.SecondAddress)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("second_address");

                entity.Property(e => e.State)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("state");

                entity.Property(e => e.TypeCompanyId).HasColumnName("type_company_id");

                entity.Property(e => e.TypeIdentificationId).HasColumnName("type_identification_id");

                entity.HasOne(d => d.MainContactTypeIdentification)
                    .WithMany(p => p.CompanyMainContactTypeIdentifications)
                    .HasForeignKey(d => d.MainContactTypeIdentificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Companies_Main_Contact_Types_Identificaction");

                entity.HasOne(d => d.TypeCompany)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.TypeCompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Companies_type_company");

                entity.HasOne(d => d.TypeIdentification)
                    .WithMany(p => p.CompanyTypeIdentifications)
                    .HasForeignKey(d => d.TypeIdentificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Companies_Types_Identificaction");
            });

            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.ToTable("favorites");

                entity.Property(e => e.FavoriteId).HasColumnName("favorite_id");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MenuId).HasColumnName("menu_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_favorites_menus");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_favorites_users");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("genders");

                entity.Property(e => e.GenderId).HasColumnName("gender_id");

                entity.Property(e => e.GenderName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("gender_name");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");
            });
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("invoices");

                entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_date");

                entity.Property(e => e.InvoiceDate)
                    .HasColumnType("date")
                    .HasColumnName("invoice_date");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.OrderStatusId).HasColumnName("order_status_id");

                entity.Property(e => e.PaymentDueDate)
                    .HasColumnType("date")
                    .HasColumnName("payment_due_date");

                entity.Property(e => e.PayrollDeduction)
                    .HasColumnType("money")
                    .HasColumnName("payroll_deduction");

                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("money")
                    .HasColumnName("total_amount");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.InvoiceCompanies)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_invoices_companies");

                entity.HasOne(d => d.OrderStatus)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.OrderStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_invoices_order_status");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.InvoiceSuppliers)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_invoices_companies1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_invoices_users");

                entity.Property(e => e.RowVersion)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken();
            });

            modelBuilder.Entity<InvoicesDetail>(entity =>
            {
                entity.HasKey(e => e.InvoiceDetailId)
                    .HasName("PK_InvoiceDetailsId2");

                entity.ToTable("invoices_details");

                entity.Property(e => e.InvoiceDetailId).HasColumnName("invoice_detail_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_date");

                entity.Property(e => e.Details)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("details");

                entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");

                entity.Property(e => e.MenuId).HasColumnName("menu_id");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("product_name");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<LogsCreditsBalance>(entity =>
            {
                entity.HasKey(e => e.LogCreditBalanceId)
                    .HasName("PK_LogCreditBalance");

                entity.ToTable("logs_credits_balances");

                entity.Property(e => e.LogCreditBalanceId).HasColumnName("log_credit_balance_id");

                entity.Property(e => e.ChargeApplied)
                    .HasColumnType("money")
                    .HasColumnName("charge_applied");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentBalance)
                    .HasColumnType("money")
                    .HasColumnName("current_balance");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_date");

                entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");

                entity.Property(e => e.LogCreditBalanceDate)
                    .HasColumnType("date")
                    .HasColumnName("log_credit_balance_date");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.PayrollDeduction)
                    .HasColumnType("money")
                    .HasColumnName("payroll_deduction");

                entity.Property(e => e.PreviousBalance)
                    .HasColumnType("money")
                    .HasColumnName("previous_balance");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.LogsCreditsBalances)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_logs_credits_balances_companies");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.LogsCreditsBalances)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_logs_credits_balances_invoices");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LogsCreditsBalances)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_logs_credits_balances_users");
            });

            modelBuilder.Entity<LogsCreditsUser>(entity =>
            {
                entity.HasKey(e => e.LogCreditUserId)
                    .HasName("PK_Logcredituser");

                entity.ToTable("logs_credits_users");

                entity.Property(e => e.LogCreditUserId).HasColumnName("log_credit_user_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreditBalance)
                    .HasColumnType("money")
                    .HasColumnName("credit_balance");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_date");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.LimiteCreditPayroll)
                    .HasColumnType("money")
                    .HasColumnName("limite_credit_payroll");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LogsCreditsUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_logs_credits_users_users");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("menus");

                entity.Property(e => e.MenuId).HasColumnName("menu_id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_date");

                entity.Property(e => e.DeliveryTime).HasColumnName("delivery_time");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MenuAmount)
                    .HasColumnType("money")
                    .HasColumnName("menu_amount");

                entity.Property(e => e.MenuDescription)
                    .HasMaxLength(800)
                    .IsUnicode(false)
                    .HasColumnName("menu_description");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PictureOne)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("picture_one");

                entity.Property(e => e.PictureThree)
                    .IsUnicode(false)
                    .HasColumnName("picture_three");

                entity.Property(e => e.PictureTwo)
                    .IsUnicode(false)
                    .HasColumnName("picture_two");

                entity.Property(e => e.QuantityAvailable).HasColumnName("quantity_available");

                entity.Property(e => e.TypeMenuId).HasColumnName("type_menu_id");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_menus_companies");

                entity.HasOne(d => d.TypeMenu)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.TypeMenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_types_menus_companies");
            });

            modelBuilder.Entity<OrdersStatus>(entity =>
            {
                entity.HasKey(e => e.OrderStatusId)
                    .HasName("PK_order_statu");

                entity.ToTable("orders_status");

                entity.Property(e => e.OrderStatusId).HasColumnName("order_status_id");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status_name");
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.ToTable("payment_methods");

                entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PaymentMethodName)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("payment_method_name");
            });

            modelBuilder.Entity<RecoverKey>(entity =>
            {
                entity.HasKey(e => e.RecoveryId);

                entity.ToTable("RecoverKey");

                entity.Property(e => e.KeyExpirationDate).HasColumnName("Key_expiration_date");

                entity.Property(e => e.RequestDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Request_date");

                entity.Property(e => e.SecurityKey)
                    .HasMaxLength(100)
                    .HasColumnName("Security_Key");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RecoverKeys)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_RecoverKey_users");
            });

            modelBuilder.Entity<Suggestion>(entity =>
            {
                entity.ToTable("suggestions");

                entity.Property(e => e.SuggestionId).HasColumnName("suggestion_id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_date");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MenuId).HasColumnName("menu_id");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.OtherTypeSuggestion)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("other_type_suggestion");

                entity.Property(e => e.SuggestionComment)
                    .IsRequired()
                    .HasColumnName("suggestion_comment");

                entity.Property(e => e.SuggestionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("suggestion_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TypeSuggestionId).HasColumnName("type_suggestion_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Suggestions)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_suggestions_companies");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.Suggestions)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_suggestions_menus");

                entity.HasOne(d => d.TypeSuggestion)
                    .WithMany(p => p.Suggestions)
                    .HasForeignKey(d => d.TypeSuggestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_suggestions_type_suggestions");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Suggestions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_suggestions_users");
            });

            modelBuilder.Entity<SupplierCompany>(entity =>
            {
                entity.ToTable("supplier_company");

                entity.Property(e => e.SupplierCompanyId).HasColumnName("supplier_company_id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.SupplierCompanyCompanies)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_supplier_company_companies1");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.SupplierCompanySuppliers)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_supplier_company_companies");
            });

            modelBuilder.Entity<TypeSuggestion>(entity =>
            {
                entity.ToTable("type_suggestions");

                entity.Property(e => e.TypeSuggestionId).HasColumnName("type_suggestion_id");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsOther).HasColumnName("is_other");

                entity.Property(e => e.TypeSuggestionName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("type_suggestion_name");
            });

            modelBuilder.Entity<TypesCompany>(entity =>
            {
                entity.HasKey(e => e.TypeCompanyId)
                    .HasName("PK_type_companie");

                entity.ToTable("types_companies");

                entity.Property(e => e.TypeCompanyId).HasColumnName("type_company_id");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TypeCompanyName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("type_company_name");
            });

            modelBuilder.Entity<TypesIdentification>(entity =>
            {
                entity.HasKey(e => e.TypeIdentificationId)
                    .HasName("PK_type_identification");

                entity.ToTable("types_identifications");

                entity.Property(e => e.TypeIdentificationId).HasColumnName("type_identification_id");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TypeIdentificationName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("type_identification_name");
            });

            modelBuilder.Entity<TypesMenu>(entity =>
            {
                entity.HasKey(e => e.TypeMenuId)
                    .HasName("PK_type_menu");

                entity.ToTable("types_menus");

                entity.Property(e => e.TypeMenuId).HasColumnName("type_menu_id");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TypeMenuName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("type_menu_name");
            });


            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PK_User");

                entity.ToTable("users");

                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.AccountLocked)
                    .HasDefaultValueSql("((0))")
                    .HasColumnName("account_locked");
                entity.Property(e => e.BirthdayDate)
                    .HasColumnType("date")
                    .HasColumnName("birthday_date");
                entity.Property(e => e.CellPhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("cell_phone_number");
                entity.Property(e => e.CompanyId).HasColumnName("company_id");
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.CreationDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date");
                entity.Property(e => e.CreditAvailable)
                    .HasColumnType("money")
                    .HasColumnName("credit_available");
                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_date");
                entity.Property(e => e.DeparmentId)
                    .HasDefaultValueSql("((1))")
                    .HasColumnName("deparment_id");
               
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");
                entity.Property(e => e.FirstLastName)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("first_last_name");
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("first_name");
                entity.Property(e => e.GenderId).HasColumnName("gender_id");
                entity.Property(e => e.IsActive).HasColumnName("is_active");
                entity.Property(e => e.IsAdmin)
                    .HasDefaultValueSql("((0))")
                    .HasColumnName("is_admin");
                entity.Property(e => e.LastAccess)
                    .HasColumnType("datetime")
                    .HasColumnName("last_access");
                entity.Property(e => e.LimiteCreditPayroll)
                    .HasColumnType("money")
                    .HasColumnName("limite_credit_payroll");
                entity.Property(e => e.LoginAttempts).HasColumnName("login_attempts");
                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");
                entity.Property(e => e.MustResetPassword)
                    .HasDefaultValueSql("((0))")
                    .HasColumnName("must_reset_password");
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");
                entity.Property(e => e.PayrollDiscount)
                    .HasColumnType("money")
                    .HasColumnName("payroll_discount");
                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("phone_number");
                entity.Property(e => e.SecondLastName)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("second_last_name");
                entity.Property(e => e.SecondName)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("second_name");
                entity.Property(e => e.TypeIdentificationId).HasColumnName("type_identification_id");
                entity.Property(e => e.UserIdentification)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("user_identification");
                entity.Property(e => e.UserPhoto).HasColumnName("user_photo");
                entity.Property(e => e.UserSecurityKey)
                    .IsRequired()
                    .HasColumnName("user_security_key");
                entity.Property(e => e.UserTypeId).HasColumnName("user_type_id");

                entity.HasOne(d => d.Company).WithMany(p => p.Users)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Company");

                entity.HasOne(d => d.Deparment).WithMany(p => p.Users)
                    .HasForeignKey(d => d.DeparmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_users_deparment");

                entity.HasOne(d => d.Gender).WithMany(p => p.Users)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_users_genders");

                entity.HasOne(d => d.TypeIdentification).WithMany(p => p.Users)
                    .HasForeignKey(d => d.TypeIdentificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tipe_identification");

                entity.HasOne(d => d.UserType).WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserType");
            });

            modelBuilder.Entity<UsersType>(entity =>
            {
                entity.HasKey(e => e.UserTypeId)
                    .HasName("PK_user_type");

                entity.ToTable("users_types");

                entity.Property(e => e.UserTypeId).HasColumnName("user_type_id");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UserTypeName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("user_type_name");
            });

            modelBuilder.Entity<WeeklyDay>(entity =>
            {
                entity.HasKey(e => e.WeeklyDayId).HasName("PK_weeklys_days");

                entity.ToTable("weekly_days");

                entity.Property(e => e.WeeklyDayId).HasColumnName("weekly_day_id");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.WeeklyDayName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("weekly_day_name");
            });

            modelBuilder.Entity<WeeklyMenu>(entity =>
            {
                entity.HasKey(e => e.WeeklyMenusId)
                    .HasName("PK_weekly_menus_id");

                entity.ToTable("weekly_menus");

                entity.Property(e => e.WeeklyMenusId).HasColumnName("weekly_menus_id");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.WeeklyFrom)
                    .HasColumnType("date")
                    .HasColumnName("weekly_from");

                entity.Property(e => e.WeeklyTo)
                    .HasColumnType("date")
                    .HasColumnName("weekly_to");
            });

            modelBuilder.Entity<WeeklyMenusDetail>(entity =>
            {
                entity.HasKey(e => e.WeeklyMenusDetailsId)
                    .HasName("PK_weekly_menus_details_id");

                entity.ToTable("weekly_menus_details");

                entity.Property(e => e.WeeklyMenusDetailsId).HasColumnName("weekly_menus_details_id");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MenuId).HasColumnName("menu_id");

                entity.Property(e => e.WeeklyDay).HasColumnName("weekly_day");

                entity.Property(e => e.WeeklyMenusId).HasColumnName("weekly_menus_id");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.WeeklyMenusDetails)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_weekly_menus_details_menus");

                entity.HasOne(d => d.WeeklyDayNavigation)
                    .WithMany(p => p.WeeklyMenusDetails)
                    .HasForeignKey(d => d.WeeklyDay)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_weekly_menus_details_weekly_days");

                entity.HasOne(d => d.WeeklyMenus)
                    .WithMany(p => p.WeeklyMenusDetails)
                    .HasForeignKey(d => d.WeeklyMenusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_weekly_menus_details_weekly_menus");
            });
           
            modelBuilder.Entity<VReport>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("v_reports");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");
                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("company_name");
                entity.Property(e => e.DayCurrent).HasColumnName("day_current");
                entity.Property(e => e.Details)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("details");
                entity.Property(e => e.InvoiceDate)
                    .HasColumnType("date")
                    .HasColumnName("invoice_date");
                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");
                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("product_name");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.StatusName)
                      .IsRequired()
                      .HasMaxLength(50)
                      .IsUnicode(false)
                      .HasColumnName("status_name");
                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
                entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
                entity.Property(e => e.OrderStatusId).HasColumnName("order_status_id");
                entity.Property(e => e.SupplierName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("supplier_name");
                entity.Property(e => e.TotalAmount)
                    .HasColumnType("money")
                    .HasColumnName("total_amount");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.UserName)
                    .HasMaxLength(1158)
                    .IsUnicode(false)
                    .HasColumnName("user_name");
                entity.Property(e => e.DeparmentName)
                   .HasMaxLength(1158)
                   .IsUnicode(false)
                   .HasColumnName("deparment_name");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("images");

                entity.Property(e => e.ImageId).HasColumnName("image_id");
                entity.Property(e => e.CompanyCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("company_code");
                entity.Property(e => e.CompanyId).HasColumnName("company_id");
                entity.Property(e => e.ContentType)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("content_type");
                entity.Property(e => e.CreatedBy1).HasColumnName("created_by1");
                entity.Property(e => e.CreationDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date");
                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_date");
                entity.Property(e => e.ImageName)
                    .IsRequired()
                    .HasColumnName("image_name");
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))")
                    .HasColumnName("is_active");
                entity.Property(e => e.MenuId).HasColumnName("menu_id");
                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");
                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<VReportSuggestion>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("v_report_suggestion");

                entity.Property(e => e.CellPhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("cell_phone_number");
                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("company_name");
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");
                entity.Property(e => e.FirstLastName)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("first_last_name");
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("first_name");
                entity.Property(e => e.IsActive).HasColumnName("is_active");
                entity.Property(e => e.MenuDescription)
                    .HasMaxLength(800)
                    .IsUnicode(false)
                    .HasColumnName("menu_description");
                entity.Property(e => e.MenuId).HasColumnName("menu_id");
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.OtherTypeSuggestion)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("other_type_suggestion");
                entity.Property(e => e.SecondLastName)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("second_last_name");
                entity.Property(e => e.SecondName)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("second_name");
                entity.Property(e => e.SuggestionComment)
                    .IsRequired()
                    .HasColumnName("suggestion_comment");
                entity.Property(e => e.SuggestionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("suggestion_date");
                entity.Property(e => e.SuggestionId).HasColumnName("suggestion_id");
                entity.Property(e => e.TypeMenuId).HasColumnName("type_menu_id");
                entity.Property(e => e.TypeMenuName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("type_menu_name");
                entity.Property(e => e.TypeSuggestionId).HasColumnName("type_suggestion_id");
                entity.Property(e => e.TypeSuggestionName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("type_suggestion_name");
                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<VReportSupplier>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("v_report_supplier");

                entity.Property(e => e.ClientAddress)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("client_address");
                entity.Property(e => e.ClientBrandName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("client_brand_name");
                entity.Property(e => e.ClientEmail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("client_email");
                entity.Property(e => e.ClientEmpresa)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("client_empresa");
                entity.Property(e => e.ClientId).HasColumnName("client_id");
                entity.Property(e => e.ClientIdentification)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("client_identification");
                entity.Property(e => e.ClientNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("client_number");
                entity.Property(e => e.ClientTypeId).HasColumnName("client_type_id");
                entity.Property(e => e.DeletedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("deleted_date");
                entity.Property(e => e.EmailUser)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email_user");
                entity.Property(e => e.FirstLastName)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("first_last_name");
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("first_name");
                entity.Property(e => e.InvoiceDate)
                    .HasColumnType("date")
                    .HasColumnName("invoice_date");
                entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
                entity.Property(e => e.MenuId).HasColumnName("menu_id");
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.OrderStatusId).HasColumnName("order_status_id");
                entity.Property(e => e.PaymentDueDate)
                    .HasColumnType("date")
                    .HasColumnName("payment_due_date");
                entity.Property(e => e.PayrollDeduction)
                    .HasColumnType("money")
                    .HasColumnName("payroll_deduction");
                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.SecondLastName)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("second_last_name");
                entity.Property(e => e.SecondName)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("second_name");
                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status_name");
                entity.Property(e => e.SupplierAddress)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("supplier_address");
                entity.Property(e => e.SupplierBrandName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("supplier_brand_name");
                entity.Property(e => e.SupplierEmail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("supplier_email");
                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
                entity.Property(e => e.SupplierIdentification)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("supplier_identification");
                entity.Property(e => e.SupplierName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("supplier_name");
                entity.Property(e => e.SupplierNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("supplier_number");
                entity.Property(e => e.SupplierTypeId).HasColumnName("supplier_type_id");
                entity.Property(e => e.TotalAmount)
                    .HasColumnType("money")
                    .HasColumnName("total_amount");
                entity.Property(e => e.TypeMenuId).HasColumnName("type_menu_id");
                entity.Property(e => e.TypeMenuName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("type_menu_name");
                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<VReportSupplierGroup>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("v_report_supplier_group");

                entity.Property(e => e.ClientAddress)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("client_address");
                entity.Property(e => e.ClientBrandName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("client_brand_name");
                entity.Property(e => e.ClientEmail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("client_email");
                entity.Property(e => e.ClientEmpresa)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("client_empresa");
                entity.Property(e => e.ClientId).HasColumnName("client_id");
                entity.Property(e => e.ClientIdentification)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("client_identification");
                entity.Property(e => e.ClientNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("client_number");
                entity.Property(e => e.ClientTypeId).HasColumnName("client_type_id");
                entity.Property(e => e.MonthInvoice).HasColumnName("month_invoice");
                entity.Property(e => e.MonthInvoiceLetter)
                    .HasMaxLength(4000)
                    .HasColumnName("month_invoice_letter");
                entity.Property(e => e.SupplierAddress)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("supplier_address");
                entity.Property(e => e.SupplierBrandName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("supplier_brand_name");
                entity.Property(e => e.SupplierEmail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("supplier_email");
                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
                entity.Property(e => e.SupplierIdentification)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("supplier_identification");
                entity.Property(e => e.SupplierName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("supplier_name");
                entity.Property(e => e.SupplierNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("supplier_number");
                entity.Property(e => e.SupplierTypeId).HasColumnName("supplier_type_id");
                entity.Property(e => e.TotalAmount)
                    .HasColumnType("money")
                    .HasColumnName("total_amount");
                entity.Property(e => e.YearInvoice).HasColumnName("year_invoice");
            });

            modelBuilder.Entity<sp_week_menuResult>().HasNoKey().ToView(null);
            modelBuilder.Entity<sp_view_week_menu_supplierResult>().HasNoKey().ToView(null);
        }

    }
}
