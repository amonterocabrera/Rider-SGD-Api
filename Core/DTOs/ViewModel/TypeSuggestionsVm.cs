using System;

namespace SGDPEDIDOS.Application.DTOs.ViewModel
{
    public class TypeSuggestionsVm
    {

        public int TypeSuggestionId { get; set; }
        public string TypeSuggestionName { get; set; }
        public bool? IsActive { get; set; }
        public bool IsOther { get; set; }
    }
}
