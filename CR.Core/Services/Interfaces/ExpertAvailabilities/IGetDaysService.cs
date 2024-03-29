﻿using System.Collections.Generic;
using CR.Common.DTOs;
using CR.Core.DTOs.ExpertAvailabilities;
using CR.Core.DTOs.ResultDTOs;
using CR.Core.DTOs.ResultDTOs.Days;

namespace CR.Core.Services.Interfaces.ExpertAvailabilities
{
    public interface IGetDaysService
    {
        ResultDto<ResultGetDaysDto> Execute(long expertId);
    }
}
