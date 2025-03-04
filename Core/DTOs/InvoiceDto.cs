using SGDPEDIDOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.DTOs
{
    public class InvoiceDto
    {

        
        public int CompanyId { get; set; }

        public int SupplierId { get; set; }
        public decimal TotalAmount { get; set; }

        public decimal? PayrollDeduction { get; set; }
      
        public int OrderStatusId { get; set; }        

        public virtual ICollection<InvoicesDetailDto> InvoicesDetails { get; set; } = new List<InvoicesDetailDto>();

    }
    public class InvoiceStatusDto
    {

       

        public int CompanyId { get; set; }
        public int InvoiceId { get; set; }
        
        public int OrderStatusId { get; set; }
        

    }
}
