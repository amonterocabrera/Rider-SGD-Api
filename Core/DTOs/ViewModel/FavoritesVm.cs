using SGDPEDIDOS.Domain.Entities;
using System;

namespace SGDPEDIDOS.Application.DTOs.ViewModel
{
    public class FavoritesVm
    {
        public int FavoriteId { get; set; }

        public int UserId { get; set; }

        public int MenuId { get; set; }


        //public virtual MenusVm Menu { get; set; }

        //public virtual UsersVm User { get; set; }
    }
}
