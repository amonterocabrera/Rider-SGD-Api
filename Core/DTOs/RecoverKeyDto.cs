using System;

namespace SGDPEDIDOS.Application.DTOs
{
    public class RecoverKeyDto
    {
        //public int RecoveryId { get; set; }

        public int? UserId { get; set; }

        public string SecurityKey { get; set; }

        public DateTime? RequestDate { get; set; }

        public int? KeyExpirationDate { get; set; }

        public bool? Processed { get; set; }

        public bool? Expired { get; set; }
    }
}
