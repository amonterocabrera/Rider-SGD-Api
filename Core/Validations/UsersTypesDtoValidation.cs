using FluentValidation;
using SGDPEDIDOS.Application.DTOs;

namespace SGDPEDIDOS.Application.Validations
{
    public class UsersTypesDtoValidation : AbstractValidator<UsersTypesDto>
    {
        public UsersTypesDtoValidation()
        {
            RuleFor(x => x.UserTypeName).NotNull().WithMessage("UserTypeName no puede ser nulo");
            RuleFor(x => x.UserTypeName).NotEmpty().WithMessage("UserTypeName es requerido");
            //RuleFor(x => x.WeeklyTo).NotNull().WithMessage("WeeklyTo no puede ser nulo");
            //RuleFor(x => x.WeeklyTo).NotEmpty().WithMessage("WeeklyTo es requerido");
            //RuleFor(x => x.IsActive).NotNull().WithMessage("IsActive no puede ser nulo");
            //RuleFor(x => x.IsActive).NotEmpty().WithMessage("IsActive es requerido");

        }
    }
}