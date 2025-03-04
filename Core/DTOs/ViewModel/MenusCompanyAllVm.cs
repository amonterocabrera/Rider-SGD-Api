using SGDPEDIDOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.DTOs.ViewModel
{
    public class MenusCompanyAllVm
    {
        public int CompanyId { get; set; }
        public int WeeklyMenusId { get; set; }
        public int WeeklyDay { get; set; }
        public int MenuId { get; set; }
        public string TypeMenuName { get; set; }
        public string Name { get; set; }
        public string MenuDescription { get; set; }
        public TimeSpan DeliveryTime { get; set; }
        public decimal MenuAmount { get; set; }
        public string PictureTwo { get; set; }
        public string PictureThree { get; set; }
        public string PictureOne { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
