using System.Collections.Generic;
using System.Linq;
using CR.Common.DTOs;
using CR.Core.DTOs.Specialities;
using CR.Core.Services.Interfaces.Specialites;
using CR.DataAccess.Context;

namespace CR.Core.Services.Implementations.Specialites
{
    public class GetSpecialtiesForExpertProfileDropDownService : IGetSpecialtiesForExpertProfileDropDownService
    {
        private readonly ApplicationContext _context;

        public GetSpecialtiesForExpertProfileDropDownService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<List<SpecialityDto>> Execute()
        {
            var Specialties = _context.Specialties
                .Select(s => new SpecialityDto
                {
                    Id = s.Id,
                    Name = s.Name
                }).ToList();

            return new ResultDto<List<SpecialityDto>>()
            {
                Data = Specialties,
                IsSuccess = true
            };
        }
    }
}
