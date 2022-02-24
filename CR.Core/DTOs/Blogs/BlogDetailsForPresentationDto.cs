using System.Collections.Generic;

namespace CR.Core.DTOs.Blogs
{
    public class BlogDetailsForPresentationDto
    {
        public long id { get; set; }

        public string title { get; set; }

        public string authorIconSrc { get; set; }

        public long authorInformationId { get; set; }

        public string author { get; set; }

        public string authorDescription { get; set; }

        public string slug { get; set; }

        public string blogCategoryName { get; set; }

        public string shortDescription { get; set; }

        public string description { get; set; }

        public List<string> keyWords { get; set; }

        public string metaDescription { get; set; }

        public string canonicalAddress { get; set; }

        public string publishDate { get; set; }

        public string pictureSrc { get; set; }
    }
}
