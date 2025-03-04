using System;

namespace SGDPEDIDOS.Application.DTOs
{
    public class SuggestionsDto
    {
        //public int SuggestionId { get; set; }

        public int CompanyId { get; set; }

        public int UserId { get; set; }

        public int MenuId { get; set; }

        //public DateTime? SuggestionDate { get; set; }

        public string SuggestionComment { get; set; }

        public int TypeSuggestionId { get; set; }

        public string OtherTypeSuggestion { get; set; }

        //public bool? IsActive { get; set; }

        //public DateTime? CreationDate { get; set; }

        //public DateTime? DeletedDate { get; set; }

        //public int? CreatedBy { get; set; }

        //public DateTime? ModifiedDate { get; set; }

        //public int? ModifiedBy { get; set; }
    }
}
