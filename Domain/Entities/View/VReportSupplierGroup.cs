using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Domain.Entities.View
{
    public partial class VReportSupplierGroup
    {
        public int SupplierId { get; set; }

        public int SupplierTypeId { get; set; }

        public string SupplierIdentification { get; set; }

        public string SupplierName { get; set; }

        public string SupplierBrandName { get; set; }

        public string SupplierNumber { get; set; }

        public string SupplierEmail { get; set; }

        public string SupplierAddress { get; set; }

        public int ClientId { get; set; }

        public int ClientTypeId { get; set; }

        public string ClientIdentification { get; set; }

        public string ClientEmpresa { get; set; }

        public string ClientBrandName { get; set; }

        public string ClientNumber { get; set; }

        public string ClientEmail { get; set; }

        public string ClientAddress { get; set; }

        public decimal? TotalAmount { get; set; }

        public int? YearInvoice { get; set; }

        public int? MonthInvoice { get; set; }

        public string MonthInvoiceLetter { get; set; }
    }
}
