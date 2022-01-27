using CR.Common.DTOs;
using CR.Core.DTOs.ExpertAvailabilities;
using CR.Core.Services.Interfaces.Experts;
using CR.DataAccess.Context;
using System.Linq;

namespace CR.Core.Services.Impl.Experts
{
    public class GetExpertCallUsesService : IGetExpertCallUsesService
    {
        private readonly ApplicationContext _context;

        public GetExpertCallUsesService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<ExpertCallUsesDto> Execute(long expertInformationId)
        {
            var expertInformation = _context.ExpertInformations.FirstOrDefault(e => e.Id == expertInformationId);

            if (expertInformation == null)
            {
                return new ResultDto<ExpertCallUsesDto>()
                {
                    IsSuccess = false,
                    Message = "اطلاعات متخصص یافت نشد!!",
                    Data = null
                };
            }

            ExpertCallUsesDto expertCallUses = new()
            {
                usePhoneCall = expertInformation.UsePhoneCall,
                useTextCall = expertInformation.UseTextCall,
                useVoiceCall = expertInformation.UseVoiceCall
            };

            return new ResultDto<ExpertCallUsesDto>()
            {
                Data = expertCallUses,
                IsSuccess = true
            };
        }
    }
}
