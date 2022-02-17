namespace CR.Core.DTOs.BlogCategories
{
    public class BlogCategoryForAdminPanelDto
    {
        public long Id { get; set; }
        public string PictureSrc { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public int ShowOrder { get; set; }
        public int BlogsCount { get; set; }
        public string CreationDate { get; set; }
        public bool HasChild { get; set; }

        public ParentBlogCategoryDto ParentCategory { get; set; }
    }

    public class ParentBlogCategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int BlogsCount { get; set; }
    }
}
