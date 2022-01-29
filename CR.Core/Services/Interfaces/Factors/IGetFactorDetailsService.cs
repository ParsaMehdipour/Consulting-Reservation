﻿using CR.Common.DTOs;
using CR.Core.DTOs.Factors;

namespace CR.Core.Services.Interfaces.Factors
{
    public interface IGetFactorDetailsService
    {
        ResultDto<FactorDetailsForSiteDto> Execute(string factorNumber);
    }
}
