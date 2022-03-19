using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.IndividualInformations;
using CR.DataAccess.Enums;

namespace CR.DataAccess.Entities.ExpertInformations
{
    public class ExpertImage : BaseEntity
    {
        public string Src { get; set; }
        public ImageType ImageType { get; set; }

        #region ForeignKeys

        public long ExpertInformationId { get; set; }

        #endregion

        #region NavigationProperties

        public ExpertInformation ExpertInformation { get; set; }

        #endregion
    }
}
