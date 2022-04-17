using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Settings;
using CR.Core.DTOs.Settings;

namespace CR.Core.Services.Interfaces.Settings
{
    public interface ISettingServices
    {
        ResultDto Add(AddSettingDto request);
        ResultDto Edit(EditSettingDto request);
        ResultDto<SettingDto> Get();
    }
}
