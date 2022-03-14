namespace CR.Core.DTOs.ContactUs
{
    public class ContactUsDto
    {
        public long id { get; set; }
        public string FullName { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public bool IsRead { get; set; }
        public string CreateDate { get; set; }
    }
}
