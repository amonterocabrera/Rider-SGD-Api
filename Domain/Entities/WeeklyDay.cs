﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SGDPEDIDOS.Domain.Entities
{
    public partial class WeeklyDay
    {
        public WeeklyDay()
        {
            WeeklyMenusDetails = new HashSet<WeeklyMenusDetail>();
        }

        public int WeeklyDayId { get; set; }

        public string WeeklyDayName { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<WeeklyMenusDetail> WeeklyMenusDetails { get; set; }
    }
}