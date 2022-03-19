using CR.Common.DTOs;
using CR.Core.DTOs.Rules;
using CR.Core.Services.Interfaces.Rules;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using System.Linq;

namespace CR.Core.Services.Implementations.Rules
{
    public class GetRuleForPresentationService : IGetRuleForPresentationService
    {
        private readonly ApplicationContext _context;

        public GetRuleForPresentationService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<RuleForPresentationDto> Execute(RuleType ruleType)
        {
            var rule = _context.Rules.FirstOrDefault();

            if (rule == null)
            {
                return new ResultDto<RuleForPresentationDto>
                {
                    Data = null,
                    IsSuccess = false,
                };
            }

            var data = new RuleForPresentationDto();

            if (ruleType == RuleType.Full)
            {
                data = new RuleForPresentationDto()
                {
                    FullContent = rule.FullContent,
                };
            }
            else if (ruleType == RuleType.Payment)
            {
                data = new RuleForPresentationDto()
                {
                    FullContent = rule.PaymentContent,
                };
            }
            else if (ruleType == RuleType.Comment)
            {
                data = new RuleForPresentationDto()
                {
                    FullContent = rule.CommentContent,
                };
            }
            else if (ruleType == RuleType.Other)
            {
                data = new RuleForPresentationDto()
                {
                    FullContent = rule.OtherContent,
                };
            }

            return new ResultDto<RuleForPresentationDto>()
            {
                Data = data,
                IsSuccess = true
            };
        }
    }
}
