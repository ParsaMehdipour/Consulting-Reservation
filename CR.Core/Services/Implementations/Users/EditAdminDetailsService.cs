using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.Images;
using CR.Core.DTOs.Users;
using CR.Core.Services.Interfaces.Images;
using CR.Core.Services.Interfaces.Users;
using CR.DataAccess.Context;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.Users
{
    public class EditAdminDetailsService : IEditAdminDetailsService
    {
        private readonly ApplicationContext _context;
        private readonly IImageUploaderService _imageUploaderService;

        public EditAdminDetailsService(ApplicationContext context
        , IImageUploaderService imageUploaderService)
        {
            _context = context;
            _imageUploaderService = imageUploaderService;
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


                if (request.image != null)
                {
                    admin.IconSrc = _imageUploaderService.Execute(new UploadImageDto()
                    {
                        File = request.image,
                        Folder = "AdminImage"
                    });
                }

                admin.FirstName = request.firstName;
                admin.LastName = request.lastName;

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

                    if (request.image != null)
                    {
                        expertInformation.IconSrc = _imageUploaderService.Execute(new UploadImageDto()
                        {
                            File = request.image,
                            Folder = "AdminImage"
                        });
                    }

                    expertInformation.FirstName = request.firstName;
                    expertInformation.LastName = request.lastName;
                    expertInformation.Province = request.province;
                    expertInformation.City = request.city;
                    expertInformation.PostalCode = request.degree;
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

                    if (request.image != null)
                    {
                        consumerInformation.IconSrc = _imageUploaderService.Execute(new UploadImageDto()
                        {
                            File = request.image,
                            Folder = "AdminImage"
                        });
                    }

                    consumerInformation.FirstName = request.firstName;
                    consumerInformation.LastName = request.lastName;
                    consumerInformation.Province = request.province;
                    consumerInformation.City = request.city;
                    consumerInformation.Degree = request.degree;
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
