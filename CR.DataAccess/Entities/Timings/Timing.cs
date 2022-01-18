using System;
using System.Collections.Generic;
using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.ExpertAvailabilities;
using CR.DataAccess.Enums;

namespace CR.DataAccess.Entities.Timings
{
    public class Timing : BaseEntity
    {
        public DateTime StartTime { get; set; }
        public string StartTime_String { get; set; }
        public DateTime EndTime { get; set; }
        public string EndTime_String { get; set; }
        public TimingType TimingType { get; set; }


        #region NavigationProperties

        public List<TimeOfDay> TimeOfDays { get; set; }

        #endregion
    }
}
