using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Images;
using CR.Core.DTOs.RequestDTOs;
using CR.Core.DTOs.RequestDTOs.Specialty;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.DTOs.ResultDTOs.Specialities;
using CR.Core.DTOs.Specialities;
using CR.Core.Services.Interfaces.Images;
using CR.Core.Services.Interfaces.Specialites;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.Specialties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CR.Core.Services.Implementations.Specialites
{
    public class GetAllSpecialitiesService : IGetAllSpecialitiesService
    {
        private readonly ApplicationContext _context;
        private readonly IImageUploaderService _imageUploaderService;


        public GetAllSpecialitiesService(ApplicationContext context, IImageUploaderService imageUploaderService)
        {
            _context = context;
            _imageUploaderService = imageUploaderService;
        }

        public ResultDto<ResultGetAllSpecialitiesDto> Execute(int Page = 1, int PageSize = 20, int parentId = 0)
        {
            int rowCount = 0;
            var items = _context.Specialties.Include(_ => _.Parent).Include(_ => _.Children).AsQueryable();

            if (parentId > 0)
                items = items.Where(_ => _.ParentSpecialtyId == parentId).AsQueryable();
            else
                items = items.Where(_ => _.ParentSpecialtyId == null).AsQueryable();

            var specialities = items.Select(s => new SpecialityDto
            {
                Id = s.Id,
                Name = s.Name,
                ImageSrc = s.ImageSrc,
                CreatedDate = s.CreateDate.ToShamsi(),
                Parent = new SpecialityDto()
                {
                    Name = s.Parent.Name,
                },
                HasChildren = s.Children.Any(),
                OrderNumber = s.OrderNumber
            }).OrderByDescending(s => s.OrderNumber)
                .ToPaged(Page, PageSize, out rowCount)
                .ToList();

            return new ResultDto<ResultGetAllSpecialitiesDto>
            {
                Data = new ResultGetAllSpecialitiesDto
                {
                    Specialities = specialities,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount,
                },
                IsSuccess = true,
            };

        }

        public ResultDto<List<Specialty>> GetAllSpecialitiesForSite()
        {
            var Specialities = _context.Specialties.AsNoTrackingWithIdentityResolution()
                .Include(_ => _.Children)
                .OrderBy(_ => _.OrderNumber)
                .ToList();

            var result = Specialities.Where(_ => _.ParentSpecialtyId == null)
                .AsParallel()
                .ToList();

            return new ResultDto<List<Specialty>>()
            {
                Data = result,
                IsSuccess = true
            };
        }

        public ResultDto AddNewSpecialty(RequestAddNewSpecialtyDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var Specialty = new Specialty()
                {
                    OrderNumber = request.OrderNumber,
                    Name = request.Name,
                };

                if (request.ParentId != null)
                    Specialty.SetParent(request.ParentId.Value);

                if (request.File != null)
                {
                    var dto = new UploadImageDto
                    {
                        File = request.File,
                    };
                    var newImageSrc = _imageUploaderService.Execute(dto);
                    Specialty.ImageSrc = newImageSrc;
                }
                _context.Specialties.Add(Specialty);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "تخصص با موفقیت ایجاد شد"
                };

            }
            catch (Exception e)
            {
                transaction.Rollback();

                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطا از سمت سرور"
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }

        public ResultDto DeleteSpecialty(long SpecialtyId)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var Specialty = _context.Specialties.Include(_ => _.Children).FirstOrDefault(_ => _.Id == SpecialtyId);

                if (Specialty == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "تخصص یافت نشد"
                    };
                }

                if (Specialty.Children.Count > 0)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "ابتدا تخصص های فرزند را حذف کنید"
                    };
                }

                _context.Specialties.Remove(Specialty);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "تخصص با موفقیت حذف شد"
                };
            }
            catch (Exception e)
            {
                transaction.Rollback();

                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطا از سمت سرور"
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }

        public ResultDto EditSpecialty(RequestEditSpecialityDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var Specialty = _context.Specialties.Find(request.Id);
                if (Specialty == null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "تخصص پیدا نشد !!"
                    };
                }
                if (request.File != null)
                {
                    var dto = new UploadImageDto
                    {
                        File = request.File,
                    };
                    var newImageSrc = _imageUploaderService.Execute(dto);
                    Specialty.ImageSrc = newImageSrc;
                }
                Specialty.SetName(request.Name);
                Specialty.SetOrderNumber(request.OrderNumber);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "ویرایش با موفقیت انجام شد"
                };
            }
            catch (Exception)
            {
                transaction.Rollback();

                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطا از سمت سرور"
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }

        public ResultDto<RequestEditSpecialityDto> GetSpecialty(long id)
        {
            var Specialty = _context.Specialties.Find(id);

            if (Specialty == null)
            {
                return new ResultDto<RequestEditSpecialityDto>()
                {
                    Data = null,
                    Message = "تخصص یافت نشد",
                    IsSuccess = false
                };
            }

            var result = new RequestEditSpecialityDto()
            {
                Id = Specialty.Id,
                Name = Specialty.Name,
                OrderNumber = Specialty.OrderNumber,
            };

            return new ResultDto<RequestEditSpecialityDto>()
            {
                IsSuccess = true,
                Data = result
            };
        }


        public Select2Response GetAllAttributeTypeForSelect2(Select2Request model)
        {
            int take = 10;
            int skip = (model.page - 1) * 10;

            var items = _context.Specialties.Where(u => (string.IsNullOrEmpty(model.search) || u.Name.Contains(model.search)) && u.ParentSpecialtyId == model.parentId)
                .OrderBy(u => u.Id)
                .Select(u => new Select2Item { id = u.Id, text = u.Name, disabled = false, selected = false })
                .Skip(skip).Take(take).ToList();

            var count = _context.Specialties
               .Where(u => (string.IsNullOrEmpty(model.search) || u.Name.Contains(model.search)) && u.ParentSpecialtyId == model.parentId)
               .Count();

            return new Select2Response
            {
                items = items,
                countFiltered = count
            };
        }

        public Select2Response GetSelect2ItemsForExpert(long expertid)
        {
            var expertInformation = _context.ExpertInformations.Where(a => a.Id == expertid).FirstOrDefault();
            var items = new List<Select2Item>();
            if (expertInformation.TagID != null)
            {
                var list = expertInformation.TagID.Split(",").ToList();
                foreach (var item in list)
                {
                    var x = _context.Specialties.Where(u => u.Id == Int64.Parse(item)).Select(u => new Select2Item { id = u.Id, text = u.Name, disabled = true, selected = true }).FirstOrDefault();
                    items.Add(x);
                }
            }

            return new Select2Response
            {
                items = items,
                countFiltered = items.Count
            };

        }

    }
}