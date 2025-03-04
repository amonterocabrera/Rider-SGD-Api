using FluentValidation;
using SGDPEDIDOS.Application.DTOs;

namespace SGDPEDIDOS.Application.Validations
{
    public class WeeklyDaysDtoValidation : AbstractValidator<WeeklyDaysDto>
    {
        public WeeklyDaysDtoValidation()
        {
            //RuleFor(x => x.WeeklyFrom).NotNull().WithMessage("WeeklyFrom no puede ser nulo");
            //RuleFor(x => x.WeeklyFrom).NotEmpty().WithMessage("WeeklyFrom es requerido");
            //RuleFor(x => x.WeeklyTo).NotNull().WithMessage("WeeklyTo no puede ser nulo");
            //RuleFor(x => x.WeeklyTo).NotEmpty().WithMessage("WeeklyTo es requerido");
            //RuleFor(x => x.IsActive).NotNull().WithMessage("IsActive no puede ser nulo");
            //RuleFor(x => x.IsActive).NotEmpty().WithMessage("IsActive es requerido");
           
        }
    }
}