using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.IndividualInformations;

namespace CR.DataAccess.Entities.ExpertInformations
{
    public class ExpertPrize : BaseEntity
    {
        public string PrizeName { get; set; }
        public string Year { get; set; }

        #region ForeignKeys

        public long ExpertInformationId { get; set; }

        #endregion

        #region NavigationProperties

        public ExpertInformation ExpertInformation { get; set; }

        #endregion
    }
}
