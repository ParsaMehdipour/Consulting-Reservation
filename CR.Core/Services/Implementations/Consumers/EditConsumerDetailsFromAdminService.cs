using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.Consumers;
using CR.Core.Services.Interfaces.Consumers;
using CR.DataAccess.Context;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.Consumers
{
    public class EditConsumerDetailsFromAdminService : IEditConsumerDetailsFromAdminService
    {
        private readonly ApplicationContext _context;

        public EditConsumerDetailsFromAdminService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(ConsumerDetailsForAdminDto request)
        {

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var consumerInformation =
                    _context.ConsumerInfromations.FirstOrDefault(e => e.Id == request.consumerInformationId);

                if (consumerInformation == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "اطلاعات مشاور یافت نشد"
                    };
                }

                var consumer = _context.Users.FirstOrDefault(e => e.Id == consumerInformation.ConsumerId);

                if (consumer == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "مشاور یافت نشد"
                    };
                }

                consumerInformation.FirstName = request.firstName;
                consumerInformation.LastName = request.lastName;
                consumerInformation.Province = request.province;
                consumerInformation.City = request.city;
                consumerInformation.Degree = request.degree;
                consumerInformation.SpecificAddress = request.specificAddress;
                consumerInformation.BirthDate_String = request.birthDate_String;
                consumerInformation.BirthDate = request.birthDate_String.ToGeorgianDateTime();

                consumer.FirstName = request.firstName;
                consumer.LastName = request.lastName;
                consumer.PhoneNumber = request.phoneNumber;
                consumer.Email = request.email;

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "اطلاعات مراجعه کننده با موفقیت ویرایش"
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
