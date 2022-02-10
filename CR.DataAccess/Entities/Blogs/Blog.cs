using CR.DataAccess.Common.Entity;
using System;

namespace CR.DataAccess.Entities.Blogs
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string PictureSrc { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public int ShowOrder { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public bool Status { get; set; }
        public string MetaDescription { get; set; }
        public string CanonicalAddress { get; set; }
        public DateTime PublishDate { get; set; }

        #region Foreign Keys

        public long UserId { get; set; }
        public long BlogCategoryId { get; set; }

        #endregion

        #region Navigation Properties

        public BlogCategory BlogCategory { get; set; }

        #endregion

    }
}
