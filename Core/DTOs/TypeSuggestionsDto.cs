using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.DTOs
{
    public class TypeSuggestionsDto
    {

        //public int TypeSuggestionId { get; set; }
        public string TypeSuggestionName { get; set; }
        public bool? IsActive { get; set; }
        public bool IsOther { get; set; }
    }
}
