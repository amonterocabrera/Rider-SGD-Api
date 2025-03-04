using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.DTOs
{
    public class WeeklyMenusDetailDto
    {
        //public int WeeklyMenusDetailsId { get; set; }

        public int WeeklyMenusId { get; set; }

        public int WeeklyDay { get; set; }

        public int MenuId { get; set; }

        public bool? IsActive { get; set; }
    }
}
