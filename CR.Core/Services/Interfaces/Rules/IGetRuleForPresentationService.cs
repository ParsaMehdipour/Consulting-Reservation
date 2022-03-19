using CR.Common.DTOs;
using CR.Core.DTOs.Rules;
using CR.DataAccess.Enums;

namespace CR.Core.Services.Interfaces.Rules
{
    public interface IGetRuleForPresentationService
    {
        ResultDto<RuleForPresentationDto> Execute(RuleType ruleType);
    }
}
