using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.ExpertAvailabilities;
using CR.DataAccess.Entities.Factors;
using CR.DataAccess.Entities.IndividualInformations;
using CR.DataAccess.Enums;

namespace CR.DataAccess.Entities.Appointments
{
    public class Appointment : BaseEntity
    {
        public AppointmentStatus AppointmentStatus { get; set; }
        public CallingType CallingType { get; set; }
        public string Reason { get; set; }
        public long? Price { get; set; }


        #region Foreign Keys

        public long ExpertInformationId { get; set; }
        public long ConsumerInformationId { get; set; }
        public long TimeOfDayId { get; set; }
        public long? FactorId { get; set; }

        #endregion

        #region Navigation Properties

        public TimeOfDay TimeOfDay { get; set; }
        public ExpertInformation ExpertInformation { get; set; }
        public ConsumerInfromation ConsumerInformation { get; set; }
        public virtual Factor Factor { get; set; }

        #endregion

    }
}
