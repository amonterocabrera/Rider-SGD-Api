using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.DTOs.Settings
{
    public class EmailSettings
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string From { get; set; }
        public string Password { get; set; }
        public string HTMLTemplateRoute { get; set; }
    }
}
