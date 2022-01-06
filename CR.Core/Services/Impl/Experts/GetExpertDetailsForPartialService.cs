using System.Linq;
using CR.Common.DTOs;
using CR.Core.DTOs.Experts;
using CR.Core.Services.Interfaces.Experts;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;

namespace CR.Core.Services.Impl.Experts
{
    public class GetExpertDetailsForPartialService : IGetExpertDetailsForPartialService
    {
        private readonly ApplicationContext _context;

        public GetExpertDetailsForPartialService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ExpertForPartialDto> Execute(long expertId)
        {
            var expert = _context.Users
                .Where(u=>u.UserFlag == UserFlag.Expert)
                .Include(u=>u.ExpertInformation)
                .ThenInclude(e=>e.Specialty)
                .FirstOrDefault(e=>e.Id == expertId);

            if (expert == null)
            {
                return new ResultDto<ExpertForPartialDto>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "اطلاعات شما یافت نشد!!"
                };
            }

            var expertForPartial = new ExpertForPartialDto()
            {
                FullName = expert.ExpertInformation.FirstName + " " + expert.ExpertInformation.LastName,
                IconSrc = expert.ExpertInformation.IconSrc,
                Tag = expert.ExpertInformation.Tag
            };

            return new ResultDto<ExpertForPartialDto>()
            {
                Data = expertForPartial,
                IsSuccess = true
            };
        }
    }
}
