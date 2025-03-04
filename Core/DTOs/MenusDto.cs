using System;
using System.Collections.Generic;

namespace SGDPEDIDOS.Application.DTOs
{
    public class MenusDto
    {

        public int TypeMenuId { get; set; }
        public int MenuId { get; set; }


        public decimal MenuAmount { get; set; }

        public string MenuDescription { get; set; }
        public string Name { get; set; }

        public int QuantityAvailable { get; set; }

        public TimeSpan DeliveryTime { get; set; }       

        public bool? IsActive { get; set; }
        public List<ImagenDto> Images { get; set; }


    }

    public class CopyMenusDto
    {
       public int WeeklyMenusId { get; set; }
        // 1 = Semanal, 2 = Mensual, 3 = Anual
        public int TypeInsert { get; set; }
    }

    public class DtoAddRemoveImage
    {
        public string ImageName { get; set; }

        public string ContentType { get; set; }
        public int? MenuId { get; set; }
        public int? UserId { get; set; }
        public string url { get; set; }

    }
}