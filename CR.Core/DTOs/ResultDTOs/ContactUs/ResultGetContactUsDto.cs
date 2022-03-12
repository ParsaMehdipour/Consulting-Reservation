using CR.Core.DTOs.ContactUs;
using System.Collections.Generic;

namespace CR.Core.DTOs.ResultDTOs.ContactUs
{
    public class ResultGetContactUsDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<ContactUsDto> ContactUsDtos { get; set; }
    }
}
