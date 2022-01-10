using System;
using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.IndividualInformations;

namespace CR.DataAccess.Entities.ExpertInformations
{
    public class ExpertExperience : BaseEntity
    {
        public string ClinicName { get; set; }
        public string StartYear { get; set; }
        public string FinishYear { get; set; }
        public string Role { get; set; }

        #region ForeignKeys

        public long ExpertInformationId { get; set; }

        #endregion

        #region NavigationProperties

        public ExpertInformation ExpertInformation { get; set; }

        #endregion
    }
}
