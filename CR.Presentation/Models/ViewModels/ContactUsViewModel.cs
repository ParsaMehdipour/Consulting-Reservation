using CR.Core.DTOs.ContactUs;
using CR.Core.DTOs.RequestDTOs.ContactUs;

namespace CR.Presentation.Models.ViewModels
{
    public class ContactUsViewModel
    {
        public ContactUsContentDto ContactUsContentDto { get; set; }
        public RequestAddNewContactUsDto RequestAddNewContactUsDto { get; set; }
    }
}
