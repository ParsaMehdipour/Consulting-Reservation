using System;
using System.Linq;
using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.Users;
using CR.Core.Services.Interfaces.Users;
using CR.DataAccess.Context;

namespace CR.Core.Services.Implementations.Users
{
    public class EditAdminDetailsService : IEditAdminDetailsService
    {
        private readonly ApplicationContext _context;

        public EditAdminDetailsService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(AdminDetailsDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var admin = _context.Users.FirstOrDefault(u => u.Id == request.adminId);

                if (admin == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = " مدیریت یافت نشد !!"
                    };
                }

                if (admin.ExpertInformationId != null)
                {
                    var expertInformation =
                        _context.ExpertInformations.FirstOrDefault(e => e.Id == request.informationId);

                    if (expertInformation == null)
                    {
                        return new ResultDto()
                        {
                            IsSuccess = false,
                            Message = "اطلاعات مدیریت یافت نشد"
                        };
                    }


                    expertInformation.FirstName = request.firstName;
                    expertInformation.LastName = request.lastName;
                    expertInformation.Province = request.province;
                    expertInformation.City = request.city;
                    expertInformation.PostalCode = request.postalCode;
                    expertInformation.SpecificAddress = request.specificAddress;
                    expertInformation.BirthDate_String = request.birthDate;
                    expertInformation.BirthDate = request.birthDate.ToGeorgianDateTime();

                    admin.PhoneNumber = request.phoneNumber;
                    admin.Email = request.email;

                    _context.SaveChanges();

                    transaction.Commit();

                    return new ResultDto()
                    {
                        IsSuccess = true,
                        Message = "اطلاعات مدیریت با موفقیت ویرایش"
                    };
                }
                else
                {
                    var consumerInformation =
                        _context.ConsumerInfromations.FirstOrDefault(e => e.Id == request.informationId);

                    if (consumerInformation == null)
                    {
                        return new ResultDto()
                        {
                            IsSuccess = false,
                            Message = "اطلاعات مدیریت یافت نشد"
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


                    admin.PhoneNumber = request.phoneNumber;
                    admin.Email = request.email;

                    _context.SaveChanges();
                    transaction.Commit();

                    return new ResultDto()
                    {
                        IsSuccess = true,
                        Message = "اطلاعات مدیریت با موفقیت ویرایش"
                    };
                }
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
