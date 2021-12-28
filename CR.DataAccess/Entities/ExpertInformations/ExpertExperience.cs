using System;
using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.IndividualInformations;

namespace CR.DataAccess.Entities.ExpertInformations
{
    public class ExpertExperience : BaseEntity
    {
        public string HospitalName { get; set; }
        public DateTime StartDate { get; set; }
        public string StartDate_String { get; set; }
        public DateTime FinishDate { get; set; }
        public string FinishDate_String { get; set; }
        public string Role { get; set; }

        #region ForeignKeys

        public long ExpertInformationId { get; set; }

        #endregion

        #region NavigationProperties

        public ExpertInformation ExpertInformation { get; set; }

        #endregion
    }
}
