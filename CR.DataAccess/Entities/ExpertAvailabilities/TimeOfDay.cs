using System;
using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.Appointments;
using CR.DataAccess.Entities.IndividualInformations;
using CR.DataAccess.Enums;

namespace CR.DataAccess.Entities.ExpertAvailabilities
{
    public class TimeOfDay : BaseEntity
    {
        public string StartHour { get; set; }
        public string FinishHour { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public TimingType TimingType { get; set; }
        public long PhoneCallPrice { get; set; } = 0;
        public long VoiceCallPrice { get; set; } = 0;
        public long TextCallPrice { get; set; } = 0;
        public bool IsReserved { get; set; }

        #region ForeignKeys

        public long DayId { get; set; }
        public long? AppointmentId { get; set; }
        public long ExpertInformationId { get; set; }

        #endregion

        #region Navigation Properties
        public virtual Appointment Appointment { get; set; }
        public Day Day { get; set; }
        public ExpertInformation ExpertInformation { get; set; }

        #endregion
    }
}
