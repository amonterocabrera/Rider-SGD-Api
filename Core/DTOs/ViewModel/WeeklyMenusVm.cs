using System;

namespace SGDPEDIDOS.Application.DTOs.ViewModel
{
    public class WeeklyMenusVm
    {
        public int WeeklyMenusId { get; set; }

        public DateTime WeeklyFrom { get; set; }

        public DateTime WeeklyTo { get; set; }

        public bool? IsActive { get; set; }
    }
}
