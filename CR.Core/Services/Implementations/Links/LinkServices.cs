﻿using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Links;
using CR.Core.DTOs.RequestDTOs.Links;
using CR.Core.DTOs.ResultDTOs.Links;
using CR.Core.Services.Interfaces.Links;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.Links;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CR.Core.Services.Implementations.Links
{
    public class LinkServices : ILinkServices
    {
        private readonly ApplicationContext _context;

        public LinkServices(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetLinksForAdminPanelDto> GetAllLinksForAdminPanel(string searchKey, int Page = 1, int PageSize = 20, int ParentId = 0)
        {
            int rowCount = 0;
            var linksQueryable = _context.Links.Include(_ => _.Parent).Include(_ => _.Children).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchKey))
                linksQueryable = linksQueryable.Where(_ => _.PersianTitle.Contains(searchKey) || _.Parent.PersianTitle.Contains(searchKey)).AsQueryable();

            if (ParentId > 0)
                linksQueryable = linksQueryable.Where(_ => _.ParentLinkId == ParentId).AsQueryable();

            var links = linksQueryable.Select(_ => new LinkForAdminPanelDto()
            {
                Id = _.Id,
                PersianTitle = _.PersianTitle,
                CreatedDate = _.CreateDate.ToShamsi(),
                Parent = new LinkForAdminPanelDto()
                {
                    PersianTitle = _.Parent.PersianTitle,
                },
                HasChildren = _.Children.Any(),
                OrderNumber = _.OrderNumber
            }).AsEnumerable()
                .ToPaged(Page, PageSize, out rowCount)
                .ToList();

            return new ResultDto<ResultGetLinksForAdminPanelDto>()
            {
                Data = new ResultGetLinksForAdminPanelDto()
                {
                    LinkForAdminPanelDtos = links,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount,
                },
                IsSuccess = true
            };
        }

        public ResultDto<List<Link>> GetAllLinksForSite()
        {
            var links = _context.Links.AsNoTrackingWithIdentityResolution()
                .Include(_ => _.Children)
                .OrderBy(_ => _.OrderNumber)
                .ToList();

            var result = links.Where(_ => _.ParentLinkId == null)
                .AsParallel()
                .ToList();

            return new ResultDto<List<Link>>()
            {
                Data = result,
                IsSuccess = true
            };
        }

        public ResultDto AddNewLink(RequestAddNewLinkDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var link = new Link()
                {
                    OrderNumber = request.OrderNumber,
                    PersianTitle = request.Name,
                    SearchKey = request.SearchKey
                };

                if (request.ParentId != null)
                    link.SetParent(request.ParentId.Value);

                _context.Links.Add(link);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "لینک با موفقیت ساخته شد"
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

        public ResultDto DeleteLink(long linkId)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var link = _context.Links.Include(_ => _.Children).FirstOrDefault(_ => _.Id == linkId);

                if (link == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "لینک یافت نشد"
                    };
                }

                if (link.Children.Count > 0)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "ابتدا لینک های فرزند را حذف کنید"
                    };
                }

                _context.Links.Remove(link);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "لینک با موفقیت حذف شد"
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

        public ResultDto EditLink(RequestEditLinkDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var link = _context.Links.Find(request.Id);

                link.SetPersianTile(request.Name);
                link.SetOrderNumber(request.OrderNumber);
                link.SetSearchKey(request.SearchKey);

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

        public ResultDto<RequestEditLinkDto> GetLink(long id)
        {
            var link = _context.Links.Find(id);

            if (link == null)
            {
                return new ResultDto<RequestEditLinkDto>()
                {
                    Data = null,
                    Message = "لینک یافت نشد",
                    IsSuccess = false
                };
            }

            var result = new RequestEditLinkDto()
            {
                Id = link.Id,
                Name = link.PersianTitle,
                OrderNumber = link.OrderNumber,
                SearchKey = link.SearchKey
            };

            return new ResultDto<RequestEditLinkDto>()
            {
                IsSuccess = true,
                Data = result
            };
        }
    }
}
