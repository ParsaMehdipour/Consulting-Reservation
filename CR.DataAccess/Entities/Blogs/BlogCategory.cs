using CR.DataAccess.Common.Entity;
using System.Collections.Generic;

namespace CR.DataAccess.Entities.Blogs
{
    public class BlogCategory : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public string MetaDescription { get; set; }
        public int ShowOrder { get; set; }
        public string PictureSrc { get; set; }

        #region ForeignKeys

        public long? ParentCategoryId { get; set; }

        #endregion

        #region Navigation Properties

        public virtual BlogCategory ParentCategory { get; set; }
        public virtual ICollection<BlogCategory> SubCategories { get; set; }


        #endregion
    }
}
