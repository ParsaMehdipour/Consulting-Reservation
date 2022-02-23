namespace CR.Core.DTOs.Blogs
{
    public class BlogForPresentationDto
    {
        public string Slug { get; set; }
        public string PictureSrc { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string AuthorFullName { get; set; }
        public string AuthorIconSrc { get; set; }
        public long AuthorInformationId { get; set; }
        public string PublishDate { get; set; }
    }
}
