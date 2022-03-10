using CR.Common.DTOs;
using CR.Core.DTOs.Rules;

namespace CR.Core.Services.Interfaces.Rules
{
    public interface IGetRulesContentService
    {
        ResultDto<RuleDto> Execute();
    }
}
