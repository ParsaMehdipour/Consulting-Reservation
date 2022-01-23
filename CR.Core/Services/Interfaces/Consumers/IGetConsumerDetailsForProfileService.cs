﻿using CR.Common.DTOs;
using CR.Core.DTOs.ResultDTOs;

namespace CR.Core.Services.Interfaces.Consumers
{
    public interface IGetConsumerDetailsForProfileService
    {
        ResultDto<ResultGetConsumerDetailsForSiteDto> Execute(long consumerId);
    }
}