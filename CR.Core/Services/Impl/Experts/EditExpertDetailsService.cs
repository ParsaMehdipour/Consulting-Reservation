using System;
using System.Collections.Generic;
using System.Linq;
using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Experts;
using CR.Core.DTOs.Images;
using CR.Core.Services.Interfaces.Experts;
using CR.Core.Services.Interfaces.Images;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.ExpertInformations;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
// ReSharper disable All

namespace CR.Core.Services.Impl.Experts
{
    public class EditExpertDetailsService : IEditExpertDetailsService
    {
        private readonly ApplicationContext _context;
        private readonly IImageUploaderService _imageUploaderService;

        public EditExpertDetailsService(ApplicationContext context
        ,IImageUploaderService imageUploaderService)
        {
            _context = context;
            _imageUploaderService = imageUploaderService;
        }

        public ResultDto Execute(ExpertDetailsForProfileDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var expertInformation = _context.ExpertInformations
                    .Include(e => e.Specialty)
                    .Include(e => e.ExpertExperiences)
                    .Include(e => e.ExpertImages)
                    .Include(e => e.ExpertMemberships)
                    .Include(e => e.ExpertPrizes)
                    .Include(e => e.ExpertStudies)
                    .Include(e => e.ExpertSubscriptions)
                    .FirstOrDefault(e => e.Id == request.id);

                var expert = _context.Users.FirstOrDefault(u => u.Id == expertInformation.ExpertId);

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
                //expertInformation.IsFreeOfCharge = request.isFreeOfCharge;
                //expertInformation.Price = (request.isFreeOfCharge == true) ? 0 : Convert.ToInt32(DateConvertor.ToEnglishNumber(request.price));
                expert.Email = request.email;
                expert.PhoneNumber = request.phoneNumber;

                //ویرایش تصاویر کلینیک
                _context.ExpertImages.RemoveRange(expertInformation.ExpertImages);

                if (request.ClininImages != null)
                {
                    var expertImages = new List<ExpertImage>();

                    foreach (var image in request.ClininImages)
                    {
                        var expertImage = new ExpertImage()
                        {
                            ExpertInformation = expertInformation,
                            ExpertInformationId = expertInformation.Id,
                            Src = _imageUploaderService.Execute(new UploadImageDto()
                            {
                                File = image,
                                Folder = "Clinics"
                            })

                        };

                        expertImages.Add(expertImage);

                    }

                    _context.ExpertImages.AddRange(expertImages);

                }

                //ویرایش تجربه های پزشک
                _context.ExpertExperiences.RemoveRange(expertInformation.ExpertExperiences);

                if (request.experiences != null)
                {

                    List<ExpertExperience> expertExperiences = new List<ExpertExperience>();
                    foreach (var item in request.experiences)
                    {
                        var expertExperience = new ExpertExperience()
                        {
                            StartYear = item.startYear,
                            FinishYear = item.finishYear,
                            ClinicName = item.clinicName,
                            Role = item.role,
                            ExpertInformation = expertInformation,
                            ExpertInformationId = expertInformation.Id
                        };
                        expertExperiences.Add(expertExperience);
                    }

                    _context.ExpertExperiences.AddRange(expertExperiences);
                }

                //ویرایش جوایز پزشک
                _context.ExpertPrizes.RemoveRange(expertInformation.ExpertPrizes);

                if (request.prizes != null)
                {
                    List<ExpertPrize> expertPrizes = new List<ExpertPrize>();

                    foreach (var item in request.prizes)
                    {
                        var expertPrize = new ExpertPrize()
                        {
                            PrizeName = item.prizeName,
                            Year = item.year,
                            ExpertInformation = expertInformation,
                            ExpertInformationId = expertInformation.Id
                        };
                        expertPrizes.Add(expertPrize);
                    }

                    _context.ExpertPrizes.AddRange(expertPrizes);
                }

                //ویرایش عضویت های پزشک
                _context.ExpertMemberships.RemoveRange(expertInformation.ExpertMemberships);

                if (request.memberships != null)
                {
                    List<ExpertMembership> expertMemberships = new List<ExpertMembership>();

                    foreach (var item in request.memberships)
                    {
                        var expertMembership = new ExpertMembership()
                        {
                            Name = item.membershipName,
                            ExpertInformation = expertInformation,
                            ExpertInformationId = expertInformation.Id
                        };

                        expertMemberships.Add(expertMembership);
                    }

                    _context.ExpertMemberships.AddRange(expertMemberships);
                }

                //ویرایش تحصیلات پزشک
                _context.ExpertStudies.RemoveRange(expertInformation.ExpertStudies);

                if (request.studies != null)
                {
                    List<ExpertStudy> expertStudies = new List<ExpertStudy>();

                    foreach (var item in request.studies)
                    {
                        var expertStudy = new ExpertStudy()
                        {
                            DegreeOfEducation = item.degreeOfEducation,
                            EndDate = item.endDate,
                            University = item.university,
                            ExpertInformation = expertInformation,
                            ExpertInformationId = expertInformation.Id
                        };

                        expertStudies.Add(expertStudy);
                    }

                    _context.ExpertStudies.AddRange(expertStudies);
                }

                //ویرایش ثبت نام های پزشک
                _context.ExpertSubscriptions.RemoveRange(expertInformation.ExpertSubscriptions);

                if (request.subscriptions != null)
                {
                    List<ExpertSubscription> expertSubscriptions = new List<ExpertSubscription>();

                    foreach (var item in request.subscriptions)
                    {
                        var expertSubscription = new ExpertSubscription()
                        {
                            SubscriptionName = item.subscriptionName,
                            Year = item.subscriptionYear,
                            ExpertInformationId = expertInformation.Id,
                            ExpertInformation = expertInformation
                        };

                        expertSubscriptions.Add(expertSubscription);
                    }

                    _context.ExpertSubscriptions.AddRange(expertSubscriptions);
                }

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
