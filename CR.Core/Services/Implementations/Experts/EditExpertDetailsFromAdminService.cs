using System;
using System.Linq;
using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.Experts;
using CR.Core.Services.Interfaces.Experts;
using CR.DataAccess.Context;

namespace CR.Core.Services.Implementations.Experts
{
    public class EditExpertDetailsFromAdminService : IEditExpertDetailsFromAdminService
    {
        private readonly ApplicationContext _context;

        public EditExpertDetailsFromAdminService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(ExpertDetailsForAdminDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var expertInformation =
                    _context.ExpertInformations.FirstOrDefault(e => e.Id == request.expertInformationId);

                if (expertInformation == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "اطلاعات متخصص یافت نشد"
                    };
                }

                var expert = _context.Users.FirstOrDefault(e => e.Id == expertInformation.ExpertId);

                if (expert == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "متخصص یافت نشد"
                    };
                }

                expertInformation.FirstName = request.firstName;
                expertInformation.LastName = request.lastName;
                expertInformation.Province = request.province;
                expertInformation.City = request.city;
                expertInformation.PostalCode = request.postalCode;
                expertInformation.SpecificAddress = request.specificAddress;
                expertInformation.BirthDate_String = request.birthDate_String;
                expertInformation.BirthDate = request.birthDate_String.ToGeorgianDateTime();

                expert.PhoneNumber = request.phoneNumber;
                expert.Email = request.email;
                expert.FirstName = request.firstName;
                expert.LastName = request.lastName;

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "اطلاعات متخصص با موفقیت ویرایش"
                };
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                transaction.Dispose();
            }

        }
    }
}
