using SGDPEDIDOS.Domain.Entities;
using System;

namespace SGDPEDIDOS.Application.DTOs.ViewModel.Utils
{
    public class RecoverKeyUtils
    {
        public RecoverKey GenerarModelo(int Id_Usuario)
        {
            // SE CREA UN KEY ALEATORIO
            Guid _key = Guid.NewGuid();

            // SE CREA LA FECHA DE EXPIRACION DEL CODIGO DE SEGURIDAD
            TimeSpan _expiracionKey = DateTime.Now.AddDays(1) - new DateTime(1970, 1, 1, 0, 0, 0);

            // SE CREA UN NUEVO REGISTRO EN EL MODELO 'RECUPERAR_CLAVE'
            return new RecoverKey()
            {
                UserId = Id_Usuario,
                SecurityKey = _key.ToString(),
                RequestDate = DateTime.Now,
                KeyExpirationDate = (int)_expiracionKey.TotalSeconds,
                Processed = false,
                Expired = false,
            };
        }
    }
}
