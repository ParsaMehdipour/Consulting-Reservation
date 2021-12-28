using System;
using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.Appointments;
using CR.DataAccess.Entities.IndividualInformations;

namespace CR.DataAccess.Entities.ExpertAvailabilities
{
    public class TimeOfDay : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public long? Price { get; set; } = 0;
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
