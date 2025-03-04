using SGDPEDIDOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDPEDIDOS.Application.DTOs.ViewModel
{
    public class SuggestionsVm
    {
        public int SuggestionId { get; set; }

        public int CompanyId { get; set; }
        public string Companyname { get; set; }
        public string UserName { get; set; }
        public string TypeSuggestion { get; set; }

        public int UserId { get; set; }

        public int MenuId { get; set; }
        public string MenuName { get; set; }

        public string SuggestionComment { get; set; }

        public bool? IsActive { get; set; }

        public int TypeSuggestionId { get; set; }

        public string OtherTypeSuggestion { get; set; }
     
    }
}
