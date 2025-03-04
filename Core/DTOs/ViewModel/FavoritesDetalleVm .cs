using SGDPEDIDOS.Domain.Entities;
using System;

namespace SGDPEDIDOS.Application.DTOs.ViewModel
{
    public class FavoritesDetalleVm
    {
        public int FavoriteId { get; set; }

        public string FullName { get; set; }

        public string MenuName { get; set; }

        public string CompanyName { get; set; }

        public decimal MenuAmount { get; set; }

        public string MenuDescription { get; set; }

        public string TypeMenuName { get; set; }
    }
}
