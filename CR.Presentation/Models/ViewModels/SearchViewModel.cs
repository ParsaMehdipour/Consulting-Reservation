using System.Collections.Generic;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.DTOs.ResultDTOs.Experts;
using CR.Core.DTOs.Specialities;

namespace CR.Presentation.Models.ViewModels
{
    public class SearchViewModel
    {
        public ResultGetExpertsForSiteDto ResultGetExpertsForSiteDto { get; set; }
        public List<string> Specialities { get; set; }
    }
}
