﻿using CR.Common.DTOs;
using CR.Core.DTOs.Images;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.Services.Interfaces.Images;
using CR.Core.Services.Interfaces.Specialites;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.Specialties;
using System;

namespace CR.Core.Services.Implementations.Specialites
{
    public class AddNewSpecialityService : IAddNewSpecialityService
    {
        private readonly ApplicationContext _context;
        private readonly IImageUploaderService _imageUploaderService;

        public AddNewSpecialityService(
             ApplicationContext context
            , IImageUploaderService imageUploaderService)
        {
            _context = context;
            _imageUploaderService = imageUploaderService;
        }

        public ResultDto Execute(RequestAddNewSpecialtyDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var uploadImageDto = new UploadImageDto
                {
                    File = request.File,
                    Folder = "Speciality"
                };

                var imageSrc = _imageUploaderService.Execute(uploadImageDto);

                var speciality = new Specialty(request.Name, imageSrc);

                if (request.ParentId != null)
                    speciality.SetParent(request.ParentId.Value);

                _context.Specialties.Add(speciality);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "تخصص با موفقیت افزوده شد"
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
