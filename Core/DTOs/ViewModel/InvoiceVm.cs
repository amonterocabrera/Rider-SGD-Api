using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.DTOs.ViewModel
{
    public class InvoiceVm
    {
        public int InvoiceId { get; set; }

        public int CompanyId { get; set; }
        public int SupplierId { get; set; }

        public int UserId { get; set; }

        public DateTime InvoiceDate { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime? PaymentDueDate { get; set; }

        public decimal? PayrollDeduction { get; set; }

        public int OrderStatusId { get; set; }
        public virtual ICollection<InvoicesDetailVm> InvoicesDetails { get; set; } = new List<InvoicesDetailVm>();
    }
}
