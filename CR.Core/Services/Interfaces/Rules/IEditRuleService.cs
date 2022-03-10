using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Rule;

namespace CR.Core.Services.Interfaces.Rules
{
    public interface IEditRuleService
    {
        ResultDto Execute(RequestEditRuleDto request);
    }
}
