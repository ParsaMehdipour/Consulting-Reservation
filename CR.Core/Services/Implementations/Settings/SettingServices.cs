using CR.Common.DTOs;
using CR.Common.States;
using CR.Core.DTOs.Images;
using CR.Core.DTOs.RequestDTOs.Settings;
using CR.Core.DTOs.Settings;
using CR.Core.Services.Interfaces.Images;
using CR.Core.Services.Interfaces.Settings;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.Settings;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.Settings
{
    public class SettingServices : ISettingServices
    {
        private readonly ApplicationContext _context;
        private readonly IImageUploaderService _imageUploader;

        public SettingServices(ApplicationContext context
        , IImageUploaderService imageUploader)
        {
            _context = context;
            _imageUploader = imageUploader;
        }

        public ResultDto Add(AddSettingDto request)
        {
            var transaction = _context.Database.BeginTransaction();

            try
            {

                Setting setting = new()
                {
                    Title = request.title
                };

                if (request.favIcon != null)
                {
                    setting.FavIcon = _imageUploader.Execute(new UploadImageDto()
                    {
                        File = request.favIcon,
                        Folder = "Settings"
                    });
                }

                if (request.logo != null)
                {
                    setting.Logo = _imageUploader.Execute(new UploadImageDto()
                    {
                        File = request.logo,
                        Folder = "Settings"
                    });
                }

                _context.Settings.Add(setting);

                _context.SaveChanges();

                transaction.Commit();

                SettingState.SiteTitle = setting.Title;
                SettingState.SiteFavicon = setting.FavIcon;
                SettingState.SiteLogo = setting.Logo;

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "تنظیمات سایت با موفقیت افزوده شد"
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

        public ResultDto Edit(EditSettingDto request)
        {
            var transaction = _context.Database.BeginTransaction();

            try
            {

                Setting setting = _context.Settings.Find(request.id);

                setting.Title = request.title;

                if (request.favIcon != null)
                {
                    setting.FavIcon = _imageUploader.Execute(new UploadImageDto()
                    {
                        File = request.favIcon,
                        Folder = "Settings"
                    });
                }

                if (request.logo != null)
                {
                    setting.Logo = _imageUploader.Execute(new UploadImageDto()
                    {
                        File = request.logo,
                        Folder = "Settings"
                    });
                }

                _context.SaveChanges();

                transaction.Commit();

                SettingState.SiteTitle = setting.Title;
                SettingState.SiteFavicon = setting.FavIcon;
                SettingState.SiteLogo = setting.Logo;

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "تنظیمات سایت با موفقیت ویرایش شد"
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

        public ResultDto<SettingDto> Get()
        {
            var setting = _context.Settings.FirstOrDefault();

            if (setting == null)
            {
                return new ResultDto<SettingDto>()
                {
                    Data = null,
                    IsSuccess = true
                };
            }

            return new ResultDto<SettingDto>()
            {
                Data = new SettingDto()
                {
                    Id = setting.Id,
                    FavIcon = setting.FavIcon,
                    Logo = setting.Logo,
                    Title = setting.Title
                },
                IsSuccess = true
            };
        }
    }
}
