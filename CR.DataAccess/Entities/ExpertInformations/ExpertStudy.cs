using System;
using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.IndividualInformations;

namespace CR.DataAccess.Entities.ExpertInformations
{
    public class ExpertStudy : BaseEntity
    {
        public string University { get; set; }
        public string DegreeOfEducation { get; set; }
        public string EndDate { get; set; }

        #region ForeignKeys

        public long ExpertInformationId { get; set; }

        #endregion

        #region NavigationProperties

        public ExpertInformation ExpertInformation { get; set; }

        #endregion
    }
}
