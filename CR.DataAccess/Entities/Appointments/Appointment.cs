using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.ChatUsers;
using CR.DataAccess.Entities.ExpertAvailabilities;
using CR.DataAccess.Entities.Factors;
using CR.DataAccess.Entities.IndividualInformations;
using CR.DataAccess.Enums;
using System.Collections.Generic;

namespace CR.DataAccess.Entities.Appointments
{
    public class Appointment : BaseEntity
    {
        public AppointmentStatus AppointmentStatus { get; set; }
        public CallingType CallingType { get; set; }
        public string Reason { get; set; }
        public long? Price { get; set; }
        public long CommissionPrice { get; set; }
        public long DiscountPrice { get; set; }
        public long RawPrice { get; set; }
        public bool IsClosed { get; set; }


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
        public ICollection<ChatUser> ChatUsers { get; set; }

        #endregion

    }
}
