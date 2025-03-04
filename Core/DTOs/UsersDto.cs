using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.DTOs
{
    public class UsersDtoList
    {

       
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string FirstLastName { get; set; }

        public string SecondLastName { get; set; }

        public string Email { get; set; }                 
     
        public string UserIdentification { get; set; }


        public string PhoneNumber { get; set; }

        public string CellPhoneNumber { get; set; }
        public decimal? CreditAvailable { get; set; }
        public decimal? PayrollDiscount { get; set; }


    }

    public class CreditAvailableDtoList
    {


        public int UserId { get; set; }      
        public decimal? CreditAvailable { get; set; }
        public decimal? PayrollDiscount { get; set; }
        public decimal? LimiteCreditPayroll { get; set; }

    }
}
