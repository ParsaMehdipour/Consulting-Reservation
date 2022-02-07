using CR.Common.DTOs;

namespace CR.Core.Services.Interfaces.Factors
{
    public interface IUpdateFactorRefIdService
    {
        ResultDto Execute(string factorNumber, string refId);
    }
}
