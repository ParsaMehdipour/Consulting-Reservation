using System;
using System.Collections.Generic;
using System.Linq;
using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.Services.Interfaces.Experts;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.ExpertInformations;
using Microsoft.EntityFrameworkCore;

namespace CR.Core.Services.Implementations.Experts
{
    public class EditAdvancedExpertDetailsService : IEditAdvancedExpertDetailsService
    {
        private readonly ApplicationContext _context;

        public EditAdvancedExpertDetailsService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestEditAdvancedExpertDetailsDto request)
        {

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var expertInformation = _context.ExpertInformations
                    .Include(e => e.ExpertExperiences)
                    .Include(e => e.ExpertImages)
                    .Include(e => e.ExpertMemberships)
                    .Include(e => e.ExpertPrizes)
                    .Include(e => e.ExpertStudies)
                    .Include(e => e.ExpertSubscriptions)
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

                if (expert.ExpertInformation.ExpertAppointments.Any(e =>
                        e.TimeOfDay.Day.Date.Date > DateTime.Now && e.TimeOfDay.IsReserved == true))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "به دلیل داشتن نوبت های گرفته شده امکان ویرایش برای شما وجود ندارد"
                    };
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

                expert.IsActive = false;

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "ویرایش با موفقیت انجام شد پس از تایید مدیریت اکانت شما فعال می شود"
                };
            }
            catch (Exception)
            {
                transaction.Rollback();

                return new ResultDto()
                {
                    Message = "عملیات با مشکل مواجه شد!!",
                    IsSuccess = false
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
