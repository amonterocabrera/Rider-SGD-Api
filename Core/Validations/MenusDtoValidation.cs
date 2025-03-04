using FluentValidation;
using SGDPEDIDOS.Application.DTOs;

namespace SGDPEDIDOS.Application.Validations
{
    public class MenusDtoValidation : AbstractValidator<MenusDto>
    {
        public MenusDtoValidation()
        {
            //RuleFor(x => x.NombreRoles).NotNull().WithMessage("Role esta vacio");
            //RuleFor(x => x.NombreRoles).NotEmpty().WithMessage("Role es requerido");
            //RuleFor(x => x.Prioridad).Length(0, 150).WithMessage("Prioridad lenght 0 to 150 caracters");
        }
    }
}