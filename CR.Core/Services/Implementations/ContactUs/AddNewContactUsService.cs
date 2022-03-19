using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.ContactUs;
using CR.Core.Services.Interfaces.ContactUs;
using CR.DataAccess.Context;
using System;

namespace CR.Core.Services.Implementations.ContactUs
{
    public class AddNewContactUsService : IAddNewContactUsService
    {
        private readonly ApplicationContext _context;

        public AddNewContactUsService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestAddNewContactUsDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var contactUs = new DataAccess.Entities.ContactUs.ContactUs()
                {
                    FullName = request.fullName,
                    PhoneNumber = request.phone,
                    Email = request.email,
                    Message = request.message
                };

                _context.ContactUs.Add(contactUs);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "پیام شما با موفقیت به مدیریت رسید"
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
        }
    }
}
