using System;
using System.Linq;
using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.Images;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.Services.Interfaces.Consumers;
using CR.Core.Services.Interfaces.Images;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;

namespace CR.Core.Services.Implementations.Consumers
{
    public class EditConsumerDetailsService : IEditConsumerDetailsService
    {
        private readonly ApplicationContext _context;
        private readonly IImageUploaderService _imageUploaderService;

        public EditConsumerDetailsService(ApplicationContext context
        , IImageUploaderService imageUploaderService)
        {
            _context = context;
            _imageUploaderService = imageUploaderService;
        }

        public ResultDto Execute(RequestEditConsumerDetailsDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var consumer = _context.Users.FirstOrDefault(u => u.Id == request.ConsumerId);

                if (consumer == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "کاربر یافت نشد!!"
                    };
                }

                var consumerInformation =
                    _context.ConsumerInfromations.FirstOrDefault(c => c.Id == request.ConsumerInformationId);

                if (consumerInformation == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "اطلاعات کاربر یافت نشد!!"
                    };
                }

                consumerInformation.FirstName = request.FirstName;
                consumerInformation.LastName = request.LastName;
                consumerInformation.BirthDate = request.BirthDate_String.ToGeorgianDateTime();
                consumerInformation.BirthDate_String = request.BirthDate_String;
                consumerInformation.Province = request.Province;
                consumerInformation.City = request.City;
                consumerInformation.SpecificAddress = request.SpecificAddress;
                consumerInformation.PostalCode = request.PostalCode;
                if (request.IconImage != null)
                {
                    string iconSrc = _imageUploaderService.Execute(new UploadImageDto()
                    {
                        File = request.IconImage,
                        Folder = "Consumer"
                    });

                    consumerInformation.IconSrc = iconSrc;
                    consumer.IconSrc = iconSrc;
                }

                if (request.gender != 0)
                {
                    consumerInformation.Gender = request.gender;
                }
                else
                {
                    consumerInformation.Gender = GenderType.Male;
                }

                consumer.FirstName = request.FirstName;
                consumer.LastName = request.LastName;
                consumer.Email = request.Email;

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "اطلاعات با موفقیت ویرایش شد"
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
