using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.DTOs.ViewModel
{
    public class MenusVm
    {
        public int MenuId { get; set; }

        public int TypeMenuId { get; set; }
        public string TypeMenuName { get; set; }

        public int SupplierId { get; set; }
       

        public decimal MenuAmount { get; set; }

        public string MenuDescription { get; set; }
        public string Name { get; set; }

        public int QuantityAvailable { get; set; }

        public TimeSpan DeliveryTime { get; set; }

        public string PictureOne { get; set; }

        public bool? IsActive { get; set; }
        public bool? IsFavorite { get; set; }

     

        public int? ModifiedBy { get; set; }
        public int? WeeklyMenuId { get; set; }
        public int? WeeklyDayId { get; set; }        
        public List<ImagenDto> Images { get; set; }

    }
    
}
