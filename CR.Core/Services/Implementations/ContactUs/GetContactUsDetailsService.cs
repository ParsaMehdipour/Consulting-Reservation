using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.ContactUs;
using CR.Core.Services.Interfaces.ContactUs;
using CR.DataAccess.Context;

namespace CR.Core.Services.Implementations.ContactUs
{
    public class GetContactUsDetailsService : IGetContactUsDetailsService
    {
        private readonly ApplicationContext _context;

        public GetContactUsDetailsService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ContactUsDetailsForAdminPanel> Execute(long id)
        {
            var contactUs = _context.ContactUs.Find(id);

            if (contactUs == null)
            {
                return new ResultDto<ContactUsDetailsForAdminPanel>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "متن یافت نشد!!"
                };
            }

            contactUs.IsRead = true;

            _context.SaveChanges();

            var result = new ContactUsDetailsForAdminPanel()
            {
                fullName = contactUs.FullName,
                message = contactUs.Message,
                createDate = contactUs.CreateDate.ToShamsi(),
                email = contactUs.Email,
                phoneNumber = contactUs.PhoneNumber
            };

            return new ResultDto<ContactUsDetailsForAdminPanel>()
            {
                Data = result,
                IsSuccess = true
            };
        }
    }
}
