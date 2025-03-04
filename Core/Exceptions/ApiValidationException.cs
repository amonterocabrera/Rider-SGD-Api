using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace SGDPEDIDOS.Application.Exceptions
{
    public class ApiValidationException : Exception
    {
        public ApiValidationException() : base("Se Produjo un Error de Validación")
        {
            Errors = new List<string>();
        }
        public List<string> Errors { get; }
        public ApiValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }
        }
    }
}
