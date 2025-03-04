﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SGDPEDIDOS.Domain.Entities
{
    public partial class Suggestion
    {
        public int SuggestionId { get; set; }
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public int MenuId { get; set; }
        public DateTime? SuggestionDate { get; set; }
        public string SuggestionComment { get; set; }
        public int TypeSuggestionId { get; set; }
        public string OtherTypeSuggestion { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual Company Company { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual TypeSuggestion TypeSuggestion { get; set; }
        public virtual User User { get; set; }
    }
}