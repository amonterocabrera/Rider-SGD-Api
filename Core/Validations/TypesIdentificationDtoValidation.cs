using FluentValidation;
using SGDPEDIDOS.Application.DTOs;

namespace SGDPEDIDOS.Application.Validations
{
    public class TypesIdentificationDtoValidation : AbstractValidator<TypesIdentificationsDto>
    {
        public TypesIdentificationDtoValidation()
        {
            RuleFor(x => x.TypeIdentificationName).NotNull().WithMessage("TypeIdentificationName no puede ser nulo");
            RuleFor(x => x.TypeIdentificationName).NotEmpty().WithMessage("TypeIdentificationName es requerido");
            //RuleFor(x => x.WeeklyTo).NotNull().WithMessage("WeeklyTo no puede ser nulo");
            //RuleFor(x => x.WeeklyTo).NotEmpty().WithMessage("WeeklyTo es requerido");
            //RuleFor(x => x.IsActive).NotNull().WithMessage("IsActive no puede ser nulo");
            //RuleFor(x => x.IsActive).NotEmpty().WithMessage("IsActive es requerido");

        }
    }
}