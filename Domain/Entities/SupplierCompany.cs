﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SGDPEDIDOS.Domain.Entities { 

public partial class SupplierCompany
{
        public int SupplierCompanyId { get; set; }
        public int CompanyId { get; set; }
        public int SupplierId { get; set; }
        public bool? IsActive { get; set; }

        public virtual Company Company { get; set; }
        public virtual Company Supplier { get; set; }
    }
}