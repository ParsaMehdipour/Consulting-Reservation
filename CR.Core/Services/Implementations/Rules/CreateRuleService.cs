using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Rule;
using CR.Core.Services.Interfaces.Rules;
using CR.DataAccess.Context;
using System;

namespace CR.Core.Services.Implementations.Rules
{
    public class CreateRuleService : ICreateRuleService
    {
        private readonly ApplicationContext _context;

        public CreateRuleService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestCreateRuleDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var rule = new DataAccess.Entities.Rules.Rule()
                {
                    FullContent = request.fullContent
                };

                _context.Rules.Add(rule);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "قوانین و مقررات افزوده شد"
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
        }
    }
}
