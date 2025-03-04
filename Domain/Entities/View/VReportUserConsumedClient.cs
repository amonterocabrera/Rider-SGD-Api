using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Domain.Entities.View
{
    public partial class VReportUserConsumedClient
    {
        public int? Year { get; set; }

        public int? Month { get; set; }

        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string FirstLastName { get; set; }

        public string SecondLastName { get; set; }

        public string EmailUser { get; set; }

        public decimal? Total { get; set; }

        public decimal? Deduction { get; set; }

        public int ClientId { get; set; }

        public int ClientTypeId { get; set; }

        public string ClientIdentification { get; set; }

        public string ClientEmpresa { get; set; }

        public string ClientBrandName { get; set; }

        public string ClientNumber { get; set; }

        public string ClientEmail { get; set; }

        public string ClientAddress { get; set; }
    }
}
