using CR.DataAccess.Entities.ChatUsers;
using CR.DataAccess.Entities.Comments;
using CR.DataAccess.Entities.Favorites;
using CR.DataAccess.Entities.IndividualInformations;
using CR.DataAccess.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

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
        public bool OnlineFlag { get; set; } = false;
        public byte[] RowVersion { get; set; }

        #region Foreign Keys 

        public long? ConsumerInformationId { get; set; }

        public long? ExpertInformationId { get; set; }

        #endregion


        #region Navigation Properties

        public virtual ConsumerInfromation ConsumerInfromation { get; set; }

        public virtual ExpertInformation ExpertInformation { get; set; }

        public ICollection<Favorite> Favorites { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<ChatUser> ChatUsers { get; set; }

        public ICollection<Rating> Ratings { get; set; }

        #endregion


    }
}
