using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.AboutUs;
using CR.Core.Services.Interfaces.AboutUs;
using CR.DataAccess.Context;
using System;

namespace CR.Core.Services.Implementations.AboutUs
{
    public class AddAboutUsService : IAddAboutUsService
    {
        private readonly ApplicationContext _context;

        public AddAboutUsService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestAddAboutUsDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var aboutUs = new DataAccess.Entities.AboutUs.AboutUs()
                {
                    FullContent = request.fullContent
                };

                _context.AboutUs.Add(aboutUs);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "درباره ما افزوده شد"
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
