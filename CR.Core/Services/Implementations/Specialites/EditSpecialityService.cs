using CR.Common.DTOs;
using CR.Core.DTOs.Images;
using CR.Core.DTOs.RequestDTOs.Specialty;
using CR.Core.Services.Interfaces.Images;
using CR.Core.Services.Interfaces.Specialites;
using CR.DataAccess.Context;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.Specialites
{
    public class EditSpecialityService : IEditSpecialityService
    {
        private readonly ApplicationContext _context;
        private readonly IImageUploaderService _imageUploaderService;

        public EditSpecialityService(ApplicationContext context, IImageUploaderService imageUploaderService)
        {
            _context = context;
            _imageUploaderService = imageUploaderService;
        }

        public ResultDto Execute(RequestEditSpecialityDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var speciality = _context.Specialties.FirstOrDefault(s => s.Id == request.Id);

                if (speciality == null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "تخصص پیدا نشد !!"
                    };
                }

                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    speciality.Name = request.Name;
                }

                if (request.File != null)
                {
                    var dto = new UploadImageDto
                    {
                        File = request.File,
                    };

                    var newImageSrc = _imageUploaderService.Execute(dto);

                    speciality.ImageSrc = newImageSrc;
                }

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "تخصص با موفقیت ویرایش شد"
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
