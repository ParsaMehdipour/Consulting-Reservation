using CR.DataAccess.Entities.IndividualInformations;
using CR.DataAccess.Enums;
using Microsoft.AspNetCore.Identity;
using System;

namespace CR.DataAccess.Entities.Users
{
    public class User : IdentityUser<long>
    {
        public string IconSrc { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public UserFlag UserFlag { get; set; } = 0;
        public bool IsActive { get; set; } = true;

        #region Foreign Keys 

        public long? ConsumerInformationId { get; set; }

        public long? ExpertInformationId { get; set; }

        #endregion


        #region Navigation Properties

        public virtual ConsumerInfromation ConsumerInfromation { get; set; }

        public virtual ExpertInformation ExpertInformation { get; set; }

        #endregion


    }
}
