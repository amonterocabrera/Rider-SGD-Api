﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SGDPEDIDOS.Domain.Entities
{
    public partial class User
    {
        public User()
        {
            Favorites = new HashSet<Favorite>();
            Invoices = new HashSet<Invoice>();
            LogsCreditsBalances = new HashSet<LogsCreditsBalance>();
            LogsCreditsUsers = new HashSet<LogsCreditsUser>();
            RecoverKeys = new HashSet<RecoverKey>();
            Suggestions = new HashSet<Suggestion>();
        }

        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? IsAdmin { get; set; }
        public int GenderId { get; set; }
        public DateTime? BirthdayDate { get; set; }
        public string UserPhoto { get; set; }
        public bool? AccountLocked { get; set; }
        public int? LoginAttempts { get; set; }
        public bool? MustResetPassword { get; set; }
        public int UserTypeId { get; set; }
        public int TypeIdentificationId { get; set; }
        public string UserIdentification { get; set; }
        public decimal? CreditAvailable { get; set; }
        public decimal? PayrollDiscount { get; set; }
        public decimal? LimiteCreditPayroll { get; set; }
        public string PhoneNumber { get; set; }
        public string CellPhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public string UserSecurityKey { get; set; }
        public DateTime? LastAccess { get; set; }
        public int DeparmentId { get; set; }

        public virtual Department Deparment { get; set; }
        public virtual Company Company { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual TypesIdentification TypeIdentification { get; set; }
        public virtual UsersType UserType { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<LogsCreditsBalance> LogsCreditsBalances { get; set; }
        public virtual ICollection<LogsCreditsUser> LogsCreditsUsers { get; set; }
        public virtual ICollection<RecoverKey> RecoverKeys { get; set; }
        public virtual ICollection<Suggestion> Suggestions { get; set; }
    }
}