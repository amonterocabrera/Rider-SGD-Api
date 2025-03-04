using FluentValidation;
using SGDPEDIDOS.Application.DTOs;

namespace SGDPEDIDOS.Application.Validations
{
    public class UpdatePasswordUsersDtoValidation : AbstractValidator<UpdatePasswordUsersDto>
    {
        public UpdatePasswordUsersDtoValidation()
        {
         
            RuleFor(p => p.Password).NotEmpty().WithMessage("Su contraseña no puede estar vacía")
              .MinimumLength(8).WithMessage("La longitud de su contraseña debe ser al menos 8.")
              .MaximumLength(16).WithMessage("La longitud de su contraseña no debe exceder 16.")
              .Matches(@"[A-Z]+").WithMessage("Su contraseña debe contener al menos una letra mayúscula.")
              .Matches(@"[a-z]+").WithMessage("Su contraseña debe contener al menos una letra minúscula.")
              .Matches(@"[0-9]+").WithMessage("Su contraseña debe contener al menos un número.")
              .Matches(@"[\@\!\?\*\.]+").WithMessage("Su contraseña debe contener al menos uno (@ ! ? * .).");


            //RuleFor(x => x).Custom((x, context) =>
            //{
            //    if (x.Password != x.ConfirmaContraseña)
            //    {
            //        context.AddFailure(nameof(x.Contraseña), "Las contraseñas deben coincidir");
            //    }
            //});

            //RuleFor(x => x).Custom((x, context) =>
            //{
            //    if (x.Email != x.ConfirmEmail)
            //    {
            //        context.AddFailure(nameof(x.Email), "Las Email deben coincidir");
            //    }
            //});
        }
    }
}