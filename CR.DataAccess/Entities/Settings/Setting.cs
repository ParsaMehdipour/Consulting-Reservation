using CR.DataAccess.Common.Entity;

namespace CR.DataAccess.Entities.Settings
{
    public class Setting : BaseEntity
    {
        public string Title { get; set; }
        public string FavIcon { get; set; }
        public string Logo { get; set; }
    }
}
