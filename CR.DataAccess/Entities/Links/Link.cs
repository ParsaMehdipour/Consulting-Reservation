using CR.DataAccess.Common.Entity;
using System.Collections.Generic;

namespace CR.DataAccess.Entities.Links
{
    public class Link : BaseEntity
    {

        public Link()
        {

        }

        public string PersianTitle { get; set; }
        public string SearchKey { get; set; }
        public int OrderNumber { get; set; }


        public long? ParentLinkId { get; set; }
        public Link Parent { get; set; }
        public ICollection<Link> Children { get; set; }


        public void SetPersianTile(string persianTitle)
        {
            if (PersianTitle == persianTitle)
                return;

            PersianTitle = persianTitle;
        }


        public void SetOrderNumber(int orderNumber)
        {
            if (OrderNumber == orderNumber)
                return;

            OrderNumber = orderNumber;
        }

        public void SetParent(long parentId)
        {
            if (ParentLinkId == parentId)
                return;
            else
                ParentLinkId = parentId;
        }
    }
}