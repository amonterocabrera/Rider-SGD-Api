using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.DTOs
{
    public  class VReportSupplierVM
    {
        public int SupplierId { get; set; }
        public int ClientId { get; set; }
        public string ClientIdentification { get; set; }        

        public string ClientEmpresa { get; set; }

        public string ClientBrandName { get; set; }

        public string ClientNumber { get; set; }

        public string ClientEmail { get; set; }

        public string ClientAddress { get; set; }
        public decimal TotalAmount { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }

        public int Quantity { get; set; }
        
        public DateTime InvoiceDate { get; set; }
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string FirstLastName { get; set; }

        public string SecondLastName { get; set; }

        public string StatusName { get; set; }


    }

    public class VReportSupplierGroupVM
    {
        public int SupplierId { get; set; }
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
