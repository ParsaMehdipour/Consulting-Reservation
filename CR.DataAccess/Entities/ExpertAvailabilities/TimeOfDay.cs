using System;
using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.Appointments;
using CR.DataAccess.Entities.IndividualInformations;
using CR.DataAccess.Entities.Timings;

namespace CR.DataAccess.Entities.ExpertAvailabilities
{
    public class TimeOfDay : BaseEntity
    {
        public long PhoneCallPrice { get; set; } = 0;
        public long VoiceCallPrice { get; set; } = 0;
        public long TextCall { get; set; } = 0;
        public bool IsReserved { get; set; }

        #region ForeignKeys

        public long TimingId { get; set; }
        public long DayId { get; set; }
        public long? AppointmentId { get; set; }
        public long ExpertInformationId { get; set; }

        #endregion

        #region Navigation Properties

        public virtual Timing Timing { get; set; }
        public virtual Appointment Appointment { get; set; }
        public Day Day { get; set; }
        public ExpertInformation ExpertInformation { get; set; }

        #endregion
    }
}
