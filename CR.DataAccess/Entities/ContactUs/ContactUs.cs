using CR.DataAccess.Common.Entity;

namespace CR.DataAccess.Entities.ContactUs
{
    public class ContactUs : BaseEntity
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
    }
}
