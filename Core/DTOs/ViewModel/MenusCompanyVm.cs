using SGDPEDIDOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.DTOs.ViewModel
{
    public class MenuCompany
    {
        public int CompanyId { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public TimeSpan? StartServices { get; set; }
        public TimeSpan? EndServices { get; set; }
        public ICollection<MenusVm> Menus { get; set; } = new List<MenusVm>();
    }

  
}

