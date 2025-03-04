using FluentValidation;
using SGDPEDIDOS.Application.DTOs;

namespace SGDPEDIDOS.Application.Validations
{
    public class DeparmentDtoValidation : AbstractValidator<DeparmentDto>
    {
        public DeparmentDtoValidation()
        {
            RuleFor(x => x.DeparmentName).NotNull().WithMessage("GenderName no puede ser nulo");
            RuleFor(x => x.DeparmentName).NotEmpty().WithMessage("GenderName es requerido");
           

        }
    }
}