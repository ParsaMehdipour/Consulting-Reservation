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
                    Title = request.title,
                    SiteBannerColor = request.bannercolor,
                    TopBoxText1 = request.textbox1,
                    TopBoxText2 = request.textbox2,
                    TopBoxText3 = request.textbox3
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

                if (request.banner != null)
                {
                    setting.SiteBanner = _imageUploader.Execute(new UploadImageDto()
                    {
                        File = request.banner,
                        Folder = "Settings"
                    });
                }

                if (request.footer != null)
                {
                    setting.SiteFooterLogo = _imageUploader.Execute(new UploadImageDto()
                    {
                        File = request.footer,
                        Folder = "Settings"
                    });
                }

                if (request.imagebox1 != null)
                {
                    setting.TopBoxImage1 = _imageUploader.Execute(new UploadImageDto()
                    {
                        File = request.imagebox1,
                        Folder = "Settings"
                    });
                }
                if (request.imagebox2 != null)
                {
                    setting.TopBoxImage2 = _imageUploader.Execute(new UploadImageDto()
                    {
                        File = request.imagebox2,
                        Folder = "Settings"
                    });
                }
                if (request.imagebox3 != null)
                {
                    setting.TopBoxImage3 = _imageUploader.Execute(new UploadImageDto()
                    {
                        File = request.imagebox3,
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
                setting.SiteBannerColor = request.bannercolor;
                setting.TopBoxText1 = request.textbox1;
                setting.TopBoxText2 = request.textbox2;
                setting.TopBoxText3 = request.textbox3;

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

                if (request.banner != null)
                {
                    setting.SiteBanner = _imageUploader.Execute(new UploadImageDto()
                    {
                        File = request.banner,
                        Folder = "Settings"
                    });
                }

                if (request.footer != null)
                {
                    setting.SiteFooterLogo = _imageUploader.Execute(new UploadImageDto()
                    {
                        File = request.footer,
                        Folder = "Settings"
                    });
                }

                if (request.imagebox1 != null)
                {
                    setting.TopBoxImage1 = _imageUploader.Execute(new UploadImageDto()
                    {
                        File = request.imagebox1,
                        Folder = "Settings"
                    });
                }
                if (request.imagebox2 != null)
                {
                    setting.TopBoxImage2 = _imageUploader.Execute(new UploadImageDto()
                    {
                        File = request.imagebox2,
                        Folder = "Settings"
                    });
                }
                if (request.imagebox3 != null)
                {
                    setting.TopBoxImage3 = _imageUploader.Execute(new UploadImageDto()
                    {
                        File = request.imagebox3,
                        Folder = "Settings"
                    });
                }

                _context.SaveChanges();

                transaction.Commit();

                SettingState.SiteTitle = setting.Title;
                SettingState.SiteFavicon = setting.FavIcon;
                SettingState.SiteLogo = setting.Logo;

                SettingState.SiteFooterLogo = setting.SiteFooterLogo;
                SettingState.SiteBanner = setting.SiteBanner;
                SettingState.SiteBannerColor = setting.SiteBannerColor;
                SettingState.TopBoxText1 = setting.TopBoxText1;
                SettingState.TopBoxImage1 = setting.TopBoxImage1;
                SettingState.TopBoxText2 = setting.TopBoxText2;
                SettingState.TopBoxImage2 = setting.TopBoxImage2;
                SettingState.TopBoxText3 = setting.TopBoxText3;
                SettingState.TopBoxImage3 = setting.TopBoxImage3;

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
                    Title = setting.Title,

                    Banner = setting.SiteBanner,
                    BannerColor = setting.SiteBannerColor,
                    FooterLogo = setting.SiteFooterLogo,
                    TopBoxImage1 = setting.TopBoxImage1,
                    TopBoxImage2 = setting.TopBoxImage2,
                    TopBoxImage3 = setting.TopBoxImage3,
                    TopBoxText1 = setting.TopBoxText1,
                    TopBoxText2 = setting.TopBoxText2,
                    TopBoxText3 = setting.TopBoxText3
                },
                IsSuccess = true
            };
        }
    }
}
