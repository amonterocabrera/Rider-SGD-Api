using System;
using System.Collections.Generic;

namespace SGDPEDIDOS.application.DTOs.Security
{
    public class AuthenticationResponse
    {     
        public string JWToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
