using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.ExpertAvailabilities;
using CR.DataAccess.Entities.IndividualInformations;
using CR.DataAccess.Entities.Users;
using System;
using System.Collections.Generic;

namespace CR.DataAccess.Entities.Appointments
{
    public class Appointment : BaseEntity
    {
        public bool IsActive { get; set; }
        public long? Price { get; set; }



        #region Foreign Keys

        public long ExpertInformationId { get; set; }
        public long ConsumerInformationId { get; set; }
        public long TimeOfDayId { get; set; }

        #endregion

        #region Navigation Properties

        public TimeOfDay TimeOfDay { get; set; }
        public ExpertInformation ExpertInformation { get; set; }
        public ConsumerInfromation ConsumerInformation { get; set; }

        #endregion

    }
}
