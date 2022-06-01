using CR.Common.DTOs;

namespace CR.Core.Services.Interfaces.Experts
{
    public interface IChangeExpertStatusService
    {
        ResultDto Execute(long expertId, bool activeStatus);
    }
}
