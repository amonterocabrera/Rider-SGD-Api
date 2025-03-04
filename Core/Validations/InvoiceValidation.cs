using FluentValidation;
using SGDPEDIDOS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.Validations
{
    public class InvoiceValidation : AbstractValidator<InvoiceDto>
    {
        public InvoiceValidation()
        {
            RuleFor(x => x.InvoicesDetails).NotNull().WithMessage("Necesota por lo menos un articulo");
            RuleFor(x => x.TotalAmount).NotNull().WithMessage("El monto no puede ser nulo");
            //RuleFor(x => x.UserId).NotNull().WithMessage("El UserId no puede ser nulo");
            RuleFor(x => x.CompanyId).NotNull().WithMessage("El CompanyId no puede ser nulo");

        }
    }
}
