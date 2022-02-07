namespace CR.Core.DTOs.RequestDTOs.Blogs
{
    public class RequestAddNewBlogDto
    {
        public string title { get; set; }
        public string slug { get; set; }
        public long blogCategoryId { get; set; }
        public long shortDescription { get; set; }
        public string description { get; set; }
        public int orderNumber { get; set; }
        public string keyWords { get; set; }
        public bool status { get; set; }
        public string metaDescription { get; set; }
        public string canonicalAddress { get; set; }
        public string publishDate { get; set; }
    }
}
