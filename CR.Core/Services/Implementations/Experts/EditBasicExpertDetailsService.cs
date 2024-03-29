﻿using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.Images;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.Services.Interfaces.Experts;
using CR.Core.Services.Interfaces.Images;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.ExpertInformations;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.Experts
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

        public ResultDto Execute(RequestEditBasicExpertDetailsDto request, bool diActive)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var expertInformation = _context.ExpertInformations
                    .Include(e => e.Specialty)
                    .Include(e => e.ExpertAppointments)
                    .ThenInclude(a => a.TimeOfDay)
                    .ThenInclude(t => t.Day)
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

                if (expertInformation.ExpertAppointments.Any(e => e.AppointmentStatus == AppointmentStatus.Waiting && (e.TimeOfDay.StartTime >= DateTime.Now && e.TimeOfDay.IsReserved == true)))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "به دلیل داشتن نوبت های گرفته شده امکان ویرایش برای شما وجود ندارد"
                    };
                }

                if (request.otherImage != null)
                {
                    foreach (var file in request.otherImage)
                    {
                        var expertImage = new ExpertImage()
                        {
                            ExpertInformationId = expertInformation.Id,
                            ExpertInformation = expertInformation,
                            ImageType = ImageType.Other,
                            Src = _imageUploaderService.Execute(new UploadImageDto()
                            {
                                File = file,
                                Folder = "ExpertImages"
                            })
                        };

                        _context.ExpertImages.Add(expertImage);
                    }
                }


                if (request.resumeImage != null)
                {
                    foreach (var file in request.resumeImage)
                    {
                        var expertImage = new ExpertImage()
                        {
                            ExpertInformationId = expertInformation.Id,
                            ExpertInformation = expertInformation,
                            ImageType = ImageType.Resume,
                            Src = _imageUploaderService.Execute(new UploadImageDto()
                            {
                                File = file,
                                Folder = "ExpertImages"
                            })
                        };

                        _context.ExpertImages.Add(expertImage);
                    }
                }


                if (request.degreeImage != null)
                {
                    foreach (var file in request.degreeImage)
                    {
                        var expertImage = new ExpertImage()
                        {
                            ExpertInformationId = expertInformation.Id,
                            ExpertInformation = expertInformation,
                            ImageType = ImageType.Degree,
                            Src = _imageUploaderService.Execute(new UploadImageDto()
                            {
                                File = file,
                                Folder = "ExpertImages"
                            })
                        };

                        _context.ExpertImages.Add(expertImage);
                    }
                }


                if (request.identityImage != null)
                {
                    foreach (var file in request.identityImage)
                    {
                        var expertImage = new ExpertImage()
                        {
                            ExpertInformationId = expertInformation.Id,
                            ExpertInformation = expertInformation,
                            ImageType = ImageType.Identity,
                            Src = _imageUploaderService.Execute(new UploadImageDto()
                            {
                                File = file,
                                Folder = "ExpertImages"
                            })
                        };

                        _context.ExpertImages.Add(expertImage);
                    }
                }


                if (request.tag != null)
                {
                    var list = request.tag.Split(",").ToList();
                    string str = "";
                    string str2 = "";
                    foreach (var item in list)
                    {
                        var x = _context.Specialties.Where(a => a.Id == Int32.Parse(item)).FirstOrDefault().Name;
                        if (string.IsNullOrEmpty(str) && string.IsNullOrEmpty(str2))
                        {
                            str = x;
                            str2 = item;
                        }
                        else
                        {
                            str = str + "," + x;
                            str2 = str2 + "," + item;
                        }
                    }
                    request.tag = str;
                    request.tagid = str2;
                }

                var speciality = _context.Specialties.FirstOrDefault(s => s.Id == request.specialtyId);

                expertInformation.FirstName = request.firstName;
                expertInformation.Tag = request.tag;
                expertInformation.TagID = request.tagid;
                expertInformation.LastName = request.lastName;
                expertInformation.ShabaNumber = request.shabaNumber;
                expertInformation.Bio = request.bio;
                expertInformation.Specialty = speciality;
                expertInformation.SpecialtyId = speciality?.Id;
                expertInformation.BirthDate = request.birthDate_String.ToGeorgianDateTime();
                expertInformation.BirthDate_String = request.birthDate_String;
                expertInformation.City = request.city;
                expertInformation.ClinicAddress = request.clinicAddress;
                expertInformation.ClinicName = request.clinicName;
                expertInformation.Instagram = request.instagram;
                expertInformation.Gender = request.gender;
                if (request.iconImage != null)
                {
                    string iconSrc = _imageUploaderService.Execute(new UploadImageDto()
                    {
                        File = request.iconImage,
                        Folder = "Experts"
                    });

                    expertInformation.IconSrc = iconSrc;
                    expert.IconSrc = iconSrc;
                }

                if (Convert.ToInt32(request.phoneCallPrice) != expertInformation.PhoneCallPrice)
                {
                    var notReservedTimeOfDays = _context.TimeOfDays
                        .Include(_ => _.Day)
                        .Where(_ =>
                        _.ExpertInformationId == expertInformation.Id && _.IsReserved == false && _.Day.Date.Day >= DateTime.Now.Day).ToList();

                    foreach (var timeOfDay in notReservedTimeOfDays)
                    {
                        if (timeOfDay.TimingType == TimingType.ShortSpan)
                            timeOfDay.PhoneCallPrice = Convert.ToInt32(request.phoneCallPrice) * 30;
                        else if (timeOfDay.TimingType == TimingType.MediumSpan)
                            timeOfDay.PhoneCallPrice = Convert.ToInt32(request.phoneCallPrice) * 60;
                        else
                            timeOfDay.PhoneCallPrice = Convert.ToInt32(request.phoneCallPrice) * 90;
                    }
                }

                if (Convert.ToInt32(request.voiceCallPrice) != expertInformation.VoiceCallPrice)
                {
                    var notReservedTimeOfDays = _context.TimeOfDays
                        .Include(_ => _.Day)
                        .Where(_ =>
                            _.ExpertInformationId == expertInformation.Id && _.IsReserved == false && _.Day.Date.Day >= DateTime.Now.Day).ToList();

                    foreach (var timeOfDay in notReservedTimeOfDays)
                    {
                        if (timeOfDay.TimingType == TimingType.ShortSpan)
                            timeOfDay.VoiceCallPrice = Convert.ToInt32(request.voiceCallPrice) * 30;
                        else if (timeOfDay.TimingType == TimingType.MediumSpan)
                            timeOfDay.VoiceCallPrice = Convert.ToInt32(request.voiceCallPrice) * 60;
                        else
                            timeOfDay.VoiceCallPrice = Convert.ToInt32(request.voiceCallPrice) * 90;
                    }
                }

                if (Convert.ToInt32(request.textCallPrice) != expertInformation.TextCallPrice)
                {
                    var notReservedTimeOfDays = _context.TimeOfDays
                        .Include(_ => _.Day)
                        .Where(_ =>
                            _.ExpertInformationId == expertInformation.Id && _.IsReserved == false && _.Day.Date.Day >= DateTime.Now.Day).ToList();

                    foreach (var timeOfDay in notReservedTimeOfDays)
                    {
                        if (timeOfDay.TimingType == TimingType.ShortSpan)
                            timeOfDay.TextCallPrice = Convert.ToInt32(request.textCallPrice) * 30;
                        else if (timeOfDay.TimingType == TimingType.MediumSpan)
                            timeOfDay.TextCallPrice = Convert.ToInt32(request.textCallPrice) * 60;
                        else
                            timeOfDay.TextCallPrice = Convert.ToInt32(request.textCallPrice) * 90;
                    }
                }

                _context.SaveChanges();

                expertInformation.Province = request.province;
                expertInformation.SpecificAddress = request.specificAddress;
                expertInformation.PostalCode = request.postalCode;
                expertInformation.UsePhoneCall = request.usePhoneCall;
                expertInformation.UseTextCall = request.useTextCall;
                expertInformation.UseVoiceCall = request.useVoiceCall;
                expertInformation.PhoneCallPrice = (request.usePhoneCall && request.phoneCallPrice != null) ? Convert.ToInt32(request.phoneCallPrice.ToEnglishNumber()) : 0;
                expertInformation.VoiceCallPrice = (request.useVoiceCall && request.voiceCallPrice != null) ? Convert.ToInt32(request.voiceCallPrice.ToEnglishNumber()) : 0;
                expertInformation.TextCallPrice = (request.useTextCall && request.textCallPrice != null) ? Convert.ToInt32(request.textCallPrice.ToEnglishNumber()) : 0;
                expert.Email = request.email;
                expert.FirstName = request.firstName;
                expert.LastName = request.lastName;

                if (diActive)
                {
                    expert.IsActive = false;
                }

                _context.SaveChanges();

                transaction.Commit();

                if (diActive)
                {
                    return new ResultDto()
                    {
                        IsSuccess = true,
                        Message = "ویرایش با موفقیت انجام شد پس از تایید مدیریت اکانت شما فعال می شود"
                    };
                }
                else
                {
                    return new ResultDto()
                    {
                        IsSuccess = true,
                        Message = "ویرایش با موفقیت انجام شد"
                    };
                }
            }
            catch (Exception)
            {
                transaction.Rollback();

                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "مشکل از سمت سرور!!"
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
