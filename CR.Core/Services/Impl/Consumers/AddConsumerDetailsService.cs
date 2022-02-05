using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.Images;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.Services.Interfaces.Consumers;
using CR.Core.Services.Interfaces.Images;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.IndividualInformations;
using CR.DataAccess.Enums;
using System;
using System.Linq;

namespace CR.Core.Services.Impl.Consumers
{
    public class AddConsumerDetailsService : IAddConsumerDetailsService
    {
        private readonly ApplicationContext _context;
        private readonly IImageUploaderService _imageUploaderService;

        public AddConsumerDetailsService(ApplicationContext context
        , IImageUploaderService imageUploaderService)
        {
            _context = context;
            _imageUploaderService = imageUploaderService;
        }

        public ResultDto Execute(RequestAddConsumerDetailsDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var consumer = _context.Users.FirstOrDefault(u => u.Id == request.consumerId);

                if (consumer == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "کاربر یافت نشد !!"
                    };
                }

                var consumerInformation = new ConsumerInfromation
                {
                    FirstName = request.firstName,
                    LastName = request.lastName,
                    BirthDate = request.birthDate_String.ToGeorgianDateTime(),
                    BirthDate_String = request.birthDate_String,
                    Province = request.province,
                    City = request.city,
                    SpecificAddress = request.specificAddress,
                    PostalCode = request.postalCode,
                    ConsumerId = request.consumerId,
                    Consumer = consumer,
                };

                consumer.FirstName = request.firstName;
                consumer.LastName = request.firstName;

                if (request.gender != 0)
                {
                    consumerInformation.Gender = request.gender;
                }
                else
                {
                    consumerInformation.Gender = GenderType.Male;
                }

                if (request.iconImage != null)
                {
                    string iconSrc = _imageUploaderService.Execute(new UploadImageDto()
                    {
                        File = request.iconImage,
                        Folder = "Consumers"
                    });

                    consumerInformation.IconSrc = iconSrc;
                    consumer.IconSrc = iconSrc;
                }

                consumer.Email = request.email;

                _context.ConsumerInfromations.Add(consumerInformation);
                _context.SaveChanges();

                consumer.ConsumerInformationId = consumerInformation.Id;

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "اطلاعات با موفقیت ذخیره شد"
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
