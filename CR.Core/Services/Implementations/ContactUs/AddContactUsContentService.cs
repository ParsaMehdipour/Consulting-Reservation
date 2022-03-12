using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.ContactUs;
using CR.Core.Services.Interfaces.ContactUs;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.ContactUs;
using System;

namespace CR.Core.Services.Implementations.ContactUs
{
    public class AddContactUsContentService : IAddContactUsContentService
    {
        private readonly ApplicationContext _context;

        public AddContactUsContentService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestAddNewContactUsContentDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var contactUsContent = new ContactUsContent()
                {
                    FullContent = request.fullContent
                };

                _context.ContactUsContents.Add(contactUsContent);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "متن تماس با ما با موفقیت افزوده شد"
                };
            }
            catch (Exception)
            {
                transaction.Rollback();

                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطا!!"
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
