using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.DTOs.ViewModel.Recoverykey
{
    public class RecoverKeyVm
    {
        public int RecoveryId { get; set; }

        public int? UserId { get; set; }

        public string SecurityKey { get; set; }

        public DateTime? RequestDate { get; set; }

        public int? KeyExpirationDate { get; set; }

        public bool? Processed { get; set; }

        public bool? Expired { get; set; }
    }
}
