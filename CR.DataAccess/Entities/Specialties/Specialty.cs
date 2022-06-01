using CR.DataAccess.Common.Entity;
using CR.DataAccess.Entities.IndividualInformations;
using System.Collections.Generic;

namespace CR.DataAccess.Entities.Specialties
{
    public class Specialty : BaseEntity
    {
        public string Name { get; set; }
        public string ImageSrc { get; set; }


        #region Navigation Properties\

        public long? ParentSpecialtyId { get; set; }
        public Specialty Parent { get; set; }
        public List<ExpertInformation> ExpertInformations { get; set; }

        #endregion

        public Specialty(string name, string imageSrc)
        {
            SetName(name);
            SetImageSrc(imageSrc);
        }

        public void SetName(string name)
        {
            if (Name == name)
                return;

            Name = name;
        }

        public void SetImageSrc(string imageSrc)
        {
            if (ImageSrc == imageSrc)
                return;

            ImageSrc = imageSrc;
        }

        public void SetParent(long parentId)
        {
            if (ParentSpecialtyId == parentId)
                return;
            else
                ParentSpecialtyId = parentId;
        }
    }
}
