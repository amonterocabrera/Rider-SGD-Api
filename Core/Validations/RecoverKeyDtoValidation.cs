using FluentValidation;
using SGDPEDIDOS.Application.DTOs;

namespace SGDPEDIDOS.Application.Validations
{
    public class RecoverKeyDtoValidation : AbstractValidator<RecoverKeyDto>
    {
        public RecoverKeyDtoValidation()
        {
            //RuleFor(x => x.IdAviso).NotNull().WithMessage("IdAviso esta vacio");
            //RuleFor(x => x.IdAviso).NotEmpty().WithMessage("IdAviso es requerido");
            //RuleFor(x => x.IdOrganizacion).NotNull().WithMessage("IdOrganizacion esta vacio");
            //RuleFor(x => x.IdOrganizacion).NotEmpty().WithMessage("IdOrganizacion es  requerido");

            //RuleFor(x => x.Prioridad).Length(0, 150).WithMessage("Prioridad lenght 0 to 150 caracters");
        }
    }
}