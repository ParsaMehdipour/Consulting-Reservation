namespace CR.Core.DTOs.Blogs
{
    public class BlogForAdminDto
    {
        public long Id { get; set; }
        public string BlogPictureSrc { get; set; }
        public string BlogCategory { get; set; }
        public string Author { get; set; }
        public bool Status { get; set; }
        public string Title { get; set; }
        public string CreateDate { get; set; }
        public string PublishDate { get; set; }
    }
}
