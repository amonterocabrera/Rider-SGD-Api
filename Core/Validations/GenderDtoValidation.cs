using FluentValidation;
using SGDPEDIDOS.Application.DTOs;

namespace SGDPEDIDOS.Application.Validations
{
    public class GenderDtoValidation : AbstractValidator<GenderDto>
    {
        public GenderDtoValidation()
        {
            RuleFor(x => x.GenderName).NotNull().WithMessage("GenderName no puede ser nulo");
            RuleFor(x => x.GenderName).NotEmpty().WithMessage("GenderName es requerido");
            //RuleFor(x => x.WeeklyTo).NotNull().WithMessage("WeeklyTo no puede ser nulo");
            //RuleFor(x => x.WeeklyTo).NotEmpty().WithMessage("WeeklyTo es requerido");
            //RuleFor(x => x.IsActive).NotNull().WithMessage("IsActive no puede ser nulo");
            //RuleFor(x => x.IsActive).NotEmpty().WithMessage("IsActive es requerido");

        }
    }
}