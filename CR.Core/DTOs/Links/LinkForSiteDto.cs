using System.Collections.Generic;

namespace CR.Core.DTOs.Links
{
    public class LinkForSiteDto
    {
        public string Name { get; set; }
        public string SearchKey { get; set; }
        public ICollection<LinkForSiteDto> Children { get; set; }
    }
}
