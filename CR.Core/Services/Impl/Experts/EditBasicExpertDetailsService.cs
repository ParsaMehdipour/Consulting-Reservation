using System;
using System.Linq;
using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.Images;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.Services.Interfaces.Experts;
using CR.Core.Services.Interfaces.Images;
using CR.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace CR.Core.Services.Impl.Experts
{
    public class EditBasicExpertDetailsService : IEditBasicExpertDetailsService
    {
        private readonly ApplicationContext _context;
        private readonly IImageUploaderService _imageUploaderService;

        public EditBasicExpertDetailsService(ApplicationContext context
        , IImageUploaderService imageUploaderService)
        {
            _context = context;
            _imageUploaderService = imageUploaderService;
        }

        public ResultDto Execute(RequestEditBasicExpertDetailsDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var expertInformation = _context.ExpertInformations
                    .Include(e => e.Specialty)
                    .FirstOrDefault(e => e.Id == request.id);

                var expert = _context.Users.FirstOrDefault(u => u.Id == expertInformation.ExpertId);

                if (expert == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "اطلاعات شما یافت نشد!"
                    };
                }

                if (expertInformation == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "اطلاعات شما پیدا نشد!!"
                    };
                }

                if (request.iconImage != null)
                {
                    if (request.iconImage.Length > 3000 * 1024)
                    {
                        return new ResultDto()
                        {
                            IsSuccess = false,
                            Message = "حجم فایل ارسالی نباید بیشتر از 3 مگابیت باشد"
                        };
                    }
                }

                if (request.specialtyId == 0)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "لطفا تخصص خود را انتخاب کنید"
                    };
                }

                var speciality = _context.Specialties.FirstOrDefault(s => s.Id == request.specialtyId);

                expertInformation.FirstName = request.firstName;
                expertInformation.Tag = request.tag;
                expertInformation.LastName = request.lastName;
                expertInformation.Bio = request.bio;
                expertInformation.Specialty = speciality;
                expertInformation.SpecialtyId = speciality.Id;
                expertInformation.BirthDate = request.birthDate_String.ToGeorgianDateTime();
                expertInformation.BirthDate_String = request.birthDate_String;
                expertInformation.City = request.city;
                expertInformation.ClinicAddress = request.clinicAddress;
                expertInformation.ClinicName = request.clinicName;
                expertInformation.Instagram = request.instagram;
                expertInformation.Gender = request.gender;
                if (request.iconImage != null)
                {
                    expertInformation.IconSrc = _imageUploaderService.Execute(new UploadImageDto()
                    {
                        File = request.iconImage,
                        Folder = "Experts"
                    });
                }
                expertInformation.Province = request.province;
                expertInformation.SpecificAddress = request.specificAddress;
                expertInformation.PostalCode = request.postalCode;
                expertInformation.UsePhoneCall = request.usePhoneCall;
                expertInformation.UseTextCall = request.useTextCall;
                expertInformation.UseVoiceCall = request.useVoiceCall;
                expertInformation.PhoneCallPrice = request.usePhoneCall ? Convert.ToInt32(request.phoneCallPrice.ToEnglishNumber()) : 0;
                expertInformation.VoiceCallPrice = request.useVoiceCall ? Convert.ToInt32(request.voiceCallPrice.ToEnglishNumber()) : 0;
                expertInformation.TextCallPrice = request.useTextCall ? Convert.ToInt32(request.textCallPrice.ToEnglishNumber()) : 0;
                expert.Email = request.email;
                expert.PhoneNumber = request.phoneNumber;


                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "اطلاعات با موفقیت ذخیره شد"
                };
            }
            catch (Exception e)
            {
                transaction.Rollback();
                var error = e.Message;
                throw;
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
