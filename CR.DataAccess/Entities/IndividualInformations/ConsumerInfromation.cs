using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.Appointments;
using CR.DataAccess.Entities.Users;
using CR.DataAccess.Enums;
using System;
using System.Collections.Generic;

namespace CR.DataAccess.Entities.IndividualInformations
{
    public class ConsumerInfromation : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderType Gender { get; set; }
        public string BirthDate_String { get; set; }
        public string SpecificAddress { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string IconSrc { get; set; }


        #region Foreign Keys

        public long ConsumerId { get; set; }

        #endregion


        #region Navigation Properties

        public User Consumer { get; set; }

        public List<Appointment> ConsumerAppointments { get; set; }
        #endregion
    }
}
