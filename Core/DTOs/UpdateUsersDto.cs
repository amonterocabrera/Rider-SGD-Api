using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.DTOs
{
    public class UpdateUsersDto
    {

        //public int UserId { get; set; }

        //public int CompanyId { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string FirstLastName { get; set; }

        public string SecondLastName { get; set; }

        public string Email { get; set; }

        //public string Password { get; set; }

        public bool? IsAdmin { get; set; }

        public int GenderId { get; set; }

        public DateTime? BirthdayDate { get; set; }

        public string UserPhoto { get; set; }

        //public bool? AccountLocked { get; set; }

        //public int? LoginAttempts { get; set; }

        //public bool? MustResetPassword { get; set; }

        //public int UserTypeId { get; set; }

        //public int TypeIdentificationId { get; set; }

        //public string UserIdentification { get; set; }

        //public decimal? CreditAvailable { get; set; }

        //public decimal? PayrollDiscount { get; set; }

        //public decimal? LimiteCreditPayroll { get; set; }

        public string PhoneNumber { get; set; }

        public string CellPhoneNumber { get; set; }

        public bool IsActive { get; set; }

        //public DateTime CreationDate { get; set; }

        //public DateTime? DeletedDate { get; set; }

        //public int? CreatedBy { get; set; }

        //public DateTime? ModifiedDate { get; set; }

        //public int? ModifiedBy { get; set; }

        //public string UserSecurityKey { get; set; }

        //public DateTime? LastAccess { get; set; }
    }

    public class UpdateSubsidyAmountDto
    {
        public decimal? CreditAvailable { get; set; }

        public decimal? PayrollDiscount { get; set; }

        public decimal? LimiteCreditPayroll { get; set; }

       
    }
}
