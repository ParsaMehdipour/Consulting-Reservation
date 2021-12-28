using System;
using System.Linq;
using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.Consumers;
using CR.Core.Services.Interfaces.Consumers;
using CR.DataAccess.Context;

namespace CR.Core.Services.Impl.Consumers
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
                        Message = "اطلاعات متخصص یافت نشد"
                    };
                }

                var consumer = _context.Users.FirstOrDefault(e => e.Id == consumerInformation.ConsumerId);

                if (consumer == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "متخصص یافت نشد"
                    };
                }

                consumerInformation.FirstName = request.firstName;
                consumerInformation.LastName = request.lastName;
                consumerInformation.Province = request.province;
                consumerInformation.City = request.city;
                consumerInformation.PostalCode = request.postalCode;
                consumerInformation.SpecificAddress = request.specificAddress;
                consumerInformation.BirthDate_String = request.birthDate;
                consumerInformation.BirthDate = request.birthDate.ToGeorgianDateTime();


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
