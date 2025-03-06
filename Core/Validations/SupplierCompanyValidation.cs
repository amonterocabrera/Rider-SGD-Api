using FluentValidation;
using SGDPEDIDOS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.Validations
{
    public class SupplierCompanyValidation : AbstractValidator<SupplierCompanyDto>
    {
        public SupplierCompanyValidation()
        {
            RuleFor(x => x.CompanyId).NotNull().WithMessage("CompanyId  es requerido");
            RuleFor(x => x.SupplierId).NotNull().WithMessage("SupplierId es requerido");
        }
    }
}
