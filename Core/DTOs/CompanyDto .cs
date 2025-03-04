using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.DTOs
{
    public class CompanyDto
    {
       
        public int TypeCompanyId { get; set; }

        public int TypeIdentificationId { get; set; }

        public string CompanyIdentification { get; set; }

        public string CompanyName { get; set; }

        public string BrandName { get; set; }

        public string FirstAddress { get; set; }

        public string SecondAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string MainContact { get; set; }

        public int MainContactTypeIdentificationId { get; set; }

        public string MainContactIdentification { get; set; }

        public string MainContactPhoneNumber { get; set; }

        public bool IsActive { get; set; }
      
    }
}
