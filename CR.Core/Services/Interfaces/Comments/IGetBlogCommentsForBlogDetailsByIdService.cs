﻿using CR.Common.DTOs;
using CR.Core.DTOs.Comments.Experts;
using System.Collections.Generic;

namespace CR.Core.Services.Interfaces.Comments
{
    public interface IGetBlogCommentsForBlogDetailsByIdService
    {
        ResultDto<List<BlogCommentDto>> Execute(long id);

    }
}