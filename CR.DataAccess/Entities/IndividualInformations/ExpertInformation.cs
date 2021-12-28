using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.Appointments;
using CR.DataAccess.Entities.ExpertAvailabilities;
using CR.DataAccess.Entities.Specialties;
using CR.DataAccess.Entities.Users;
using CR.DataAccess.Enums;
using System;
using System.Collections.Generic;
using CR.DataAccess.Entities.ExpertInformations;

namespace CR.DataAccess.Entities.IndividualInformations
{
    public class ExpertInformation : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderType Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthDate_String { get; set; }
        public string Bio { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string SpecificAddress { get; set; }
        public string PostalCode { get; set; }
        public string IconSrc { get; set; }
        public string ClinicName { get; set; }
        public string ClinicAddress { get; set; }
        public string Tag { get; set; }
        public bool IsFreeOfCharge { get; set; }
        public long? Price { get; set; } = 0;
        public string Instagram { get; set; }


        #region Foreign Keys

        public long ExpertId { get; set; }
        public long? SpecialtyId { get; set; }

        #endregion


        #region Navigation Properties

        public User Expert { get; set; }
        public virtual Specialty Specialty { get; set; }
        public List<Appointment> ExpertAppointments { get; set; }
        public List<Day> Days { get; set; }
        public List<TimeOfDay> TimeOfDays { get; set; }
        public List<ExpertImage> ExpertImages { get; set; }
        public List<ExpertExperience> ExpertExperiences { get; set; }
        public List<ExpertMembership> ExpertMemberships { get; set; }
        public List<ExpertStudy> ExpertStudies { get; set; }
        public List<ExpertPrize> ExpertPrizes { get; set; }
        public List<ExpertSubscription> ExpertSubscriptions { get; set; }

        #endregion

    }
}
