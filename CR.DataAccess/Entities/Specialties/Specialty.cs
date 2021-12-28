using CR.DataAccess.Common.Entity;
using System.Collections.Generic;
using CR.DataAccess.Entities.IndividualInformations;

namespace CR.DataAccess.Entities.Specialties
{
    public class Specialty : BaseEntity
    {
        public string Name { get; set; }
        public string ImageSrc { get; set; }


        #region Navigation Properties

        public List<ExpertInformation> ExpertInformations { get; set; }

        #endregion
    }
}
