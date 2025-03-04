using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.DTOs
{
    public class SupplierCompanyDto
    {
        public int SupplierCompanyId { get; set; }

        public int CompanyId { get; set; }

        public int SupplierId { get; set; }

        public bool? IsActive { get; set; }
    }
}
