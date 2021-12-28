using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.IndividualInformations;
using CR.DataAccess.Entities.Users;
using System;
using System.Collections.Generic;

namespace CR.DataAccess.Entities.ExpertAvailabilities
{
    public class Day : BaseEntity
    {
        public DateTime Date { get; set; }
        public string Date_String { get; set; }
        public int DayOfWeek { get; set; }

        #region ForeignKeys

        public long ExpertInformationId { get; set; }

        #endregion


        #region Navigation Properties
        public ExpertInformation ExpertInformation { get; set; }
        public List<TimeOfDay> TimeOfDays { get; set; }

        #endregion
    }
}
