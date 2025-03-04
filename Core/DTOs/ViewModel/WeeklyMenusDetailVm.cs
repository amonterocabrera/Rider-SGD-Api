using System;

namespace SGDPEDIDOS.Application.DTOs.ViewModel
{
    public class WeeklyMenusDetailVm
    {
        public int WeeklyMenusDetailsId { get; set; }

        public int WeeklyMenusId { get; set; }

        public int WeeklyDay { get; set; }

        public int MenuId { get; set; }

        public bool? IsActive { get; set; }
    }
}
