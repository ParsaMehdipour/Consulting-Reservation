using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.IndividualInformations;

namespace CR.DataAccess.Entities.ExpertInformations
{
    public class ExpertImage : BaseEntity
    {
        public string Src { get; set; }

        #region ForeignKeys

        public long ExpertInformationId { get; set; }

        #endregion

        #region NavigationProperties

        public ExpertInformation ExpertInformation { get; set; }

        #endregion
    }
}
