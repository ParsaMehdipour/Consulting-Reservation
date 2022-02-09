using CR.Common.DTOs;

namespace CR.Core.Services.Interfaces.Factors
{
    public interface IUpdateFactorCartHolderPanService
    {
        ResultDto Execute(string factorNumber, string cardHolderPAN);
    }
}
