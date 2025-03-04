using FluentValidation;
using SGDPEDIDOS.Application.DTOs;

namespace SGDPEDIDOS.Application.Validations
{
    public class UsuarioDtoListValidation : AbstractValidator<UsersDtoList>
    {
        public UsuarioDtoListValidation()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("Correo no puede estar vacio");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Correo es requerido");
            RuleFor(x => x.UserIdentification).NotEmpty().WithMessage("Correo es requerido");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Correo es requerido");
            RuleFor(x => x.FirstLastName).NotEmpty().WithMessage("Correo es requerido");
            RuleFor(x => x.CellPhoneNumber).NotEmpty().WithMessage("Correo es requerido");
            RuleFor(x => x.Email).EmailAddress().WithMessage("tiene cumplir con lo parametro de Correo");
         

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