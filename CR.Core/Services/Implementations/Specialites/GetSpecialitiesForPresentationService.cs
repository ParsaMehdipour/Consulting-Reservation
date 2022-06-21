using CR.Common.DTOs;
using CR.Core.DTOs.Specialities;
using CR.Core.Services.Interfaces.Specialites;
using CR.DataAccess.Context;
using System.Collections.Generic;
using System.Linq;

namespace CR.Core.Services.Implementations.Specialites
{
    public class GetSpecialitiesForPresentationService : IGetSpecialitiesForPresentationService
    {
        private readonly ApplicationContext _context;

        public GetSpecialitiesForPresentationService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<List<SpecialityDto>> Execute()
        {
            var specialities = _context.Specialties.Where(a => a.Parent != null)
                .Select(s => new SpecialityDto
                {
                    Id = s.Id,
                    ImageSrc = s.ImageSrc,
                    Name = s.Name
                }).Take(30).ToList();

            return new ResultDto<List<SpecialityDto>>()
            {
                Data = specialities,
                IsSuccess = true
            };
        }
    }
}
