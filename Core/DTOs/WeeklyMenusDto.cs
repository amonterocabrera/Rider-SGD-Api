using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.DTOs
{
    public class WeeklyMenusDto
    {
        //public int WeeklyMenusId { get; set; }

        public DateTime WeeklyFrom { get; set; }

        public DateTime WeeklyTo { get; set; }

        public bool? IsActive { get; set; }
    }
}
