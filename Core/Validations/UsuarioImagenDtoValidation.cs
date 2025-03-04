using FluentValidation;
using SGDPEDIDOS.Application.DTOs;

namespace SGDPEDIDOS.Application.Validations
{
    public class UsuarioImagenDtoValidation : AbstractValidator<UsersImagenDto>
    {
        public UsuarioImagenDtoValidation()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("Correo no puede estar vacio");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Correo es requerido");
            RuleFor(x => x.Email).EmailAddress().WithMessage("tiene cumplir con lo parametro de Correo");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Su contraseña no puede estar vacía")
              .MinimumLength(8).WithMessage("La longitud de su contraseña debe ser al menos 8.")
              .MaximumLength(16).WithMessage("La longitud de su contraseña no debe exceder 16.")
              .Matches(@"[A-Z]+").WithMessage("Su contraseña debe contener al menos una letra mayúscula.")
              .Matches(@"[a-z]+").WithMessage("Su contraseña debe contener al menos una letra minúscula.")
              .Matches(@"[0-9]+").WithMessage("Su contraseña debe contener al menos un número.")
              .Matches(@"[\@\!\?\*\.]+").WithMessage("Su contraseña debe contener al menos uno (@ ! ? * .).");

        }
    }
}