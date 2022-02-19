using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.IndividualInformations;
using CR.DataAccess.Entities.Users;

namespace CR.DataAccess.Entities.Favorites
{
    public class Favorite : BaseEntity
    {
        #region Foreign Keys

        public long UserId { get; set; }
        public long ExpertInformationId { get; set; }

        #endregion

        #region Navigation Properties

        public User User { get; set; }
        public ExpertInformation ExpertInformation { get; set; }

        #endregion
    }
}
