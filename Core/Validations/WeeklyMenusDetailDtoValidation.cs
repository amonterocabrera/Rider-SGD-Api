using FluentValidation;
using SGDPEDIDOS.Application.DTOs;

namespace SGDPEDIDOS.Application.Validations
{
    public class WeeklyMenusDetailDtoValidation : AbstractValidator<WeeklyMenusDetailDto>
    {
        public WeeklyMenusDetailDtoValidation()
        {
            RuleFor(x => x.WeeklyMenusId).NotNull().WithMessage("WeeklyMenusId por lo menos un articulo");
            RuleFor(x => x.WeeklyMenusId).NotEmpty().WithMessage("WeeklyMenusId es requerido");
            RuleFor(x => x.WeeklyDay).NotNull().WithMessage("WeeklyDay no puede ser nulo");
            RuleFor(x => x.WeeklyDay).NotEmpty().WithMessage("WeeklyDay es requerido");
            RuleFor(x => x.MenuId).NotNull().WithMessage("MenuId no puede ser nulo");
            RuleFor(x => x.MenuId).NotEmpty().WithMessage("MenuId es requerido");
            //RuleFor(x => x.IsActive).NotNull().WithMessage("IsActive no puede ser nulo");
        }
    }
}