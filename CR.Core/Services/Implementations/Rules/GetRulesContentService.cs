using CR.Common.DTOs;
using CR.Core.DTOs.Rules;
using CR.Core.Services.Interfaces.Rules;
using CR.DataAccess.Context;
using System.Linq;

namespace CR.Core.Services.Implementations.Rules
{
    public class GetRulesContentService : IGetRulesContentService
    {
        private readonly ApplicationContext _context;

        public GetRulesContentService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<RuleDto> Execute()
        {
            var rule = _context.Rules.FirstOrDefault();

            if (rule == null)
            {
                return new ResultDto<RuleDto>
                {
                    Data = null,
                    IsSuccess = false,
                };
            }

            var data = new RuleDto()
            {
                Id = rule.Id,
                FullContent = rule.FullContent,
                PaymentContent = rule.PaymentContent,
                CommentContent = rule.CommentContent,
                OtherContent = rule.OtherContent
            };

            return new ResultDto<RuleDto>()
            {
                Data = data,
                IsSuccess = true
            };
        }
    }
}
