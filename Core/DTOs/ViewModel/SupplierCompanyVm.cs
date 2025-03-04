using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.DTOs.ViewModel
{
    public class SupplierCompanyVm
    {
        public int SupplierCompanyId { get; set; }

        public int CompanyId { get; set; }
        public string Supplier { get; set; }

        public int SupplierId { get; set; }

        public bool? IsActive { get; set; }
    }
}
