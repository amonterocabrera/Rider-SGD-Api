using System;

namespace SGDPEDIDOS.Application.DTOs.ViewModel
{
    public class WeeklyDaysVm
    {
        public int WeeklyDayId { get; set; }

        public string WeeklyDayName { get; set; }

        public bool? IsActive { get; set; }
    }
}
