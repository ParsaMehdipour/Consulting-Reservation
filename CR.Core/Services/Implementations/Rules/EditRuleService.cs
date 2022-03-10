using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Rule;
using CR.Core.Services.Interfaces.Rules;
using CR.DataAccess.Context;
using System;

namespace CR.Core.Services.Implementations.Rules
{
    public class EditRuleService : IEditRuleService
    {
        private readonly ApplicationContext _context;

        public EditRuleService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestEditRuleDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var rule = _context.Rules.Find(request.id);

                if (rule == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "اطلاعات یافت نشد!!"
                    };
                }

                rule.FullContent = request.fullContent;

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "قوانین و مقررات با موفقیت ویرایش شد"
                };
            }
            catch (Exception)
            {
                transaction.Rollback();

                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطا!!"
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
