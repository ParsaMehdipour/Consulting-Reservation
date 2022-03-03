using CR.Common.DTOs;
using CR.Core.DTOs.Days;

namespace CR.Core.Services.Interfaces.Days
{
    public interface IEditDayDetailsService
    {
        ResultDto Execute(RequestEditDayDetaislDto request);
    }
}
