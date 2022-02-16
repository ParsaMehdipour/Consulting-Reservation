using System.Collections.Generic;
using System.Linq;
using CR.Common.DTOs;
using CR.Common.Utilities;
using CR.Core.DTOs.Consumers;
using CR.Core.Services.Interfaces.Consumers;
using CR.DataAccess.Context;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;

namespace CR.Core.Services.Implementations.Consumers
{
    public class GetLatestConsumersForAdminService : IGetLatestConsumersForAdminService
    {
        private readonly ApplicationContext _context;

        public GetLatestConsumersForAdminService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto<List<LatestConsumerForAdminDto>> Execute()
        {

            var latestConsumers = _context.Users
                .Include(u => u.ConsumerInfromation)
                .Where(u => u.UserFlag == UserFlag.Consumer && u.ConsumerInfromation != null)
                .Select(u => new LatestConsumerForAdminDto
                {
                    id = u.Id,
                    firstName = u.ConsumerInfromation.FirstName,
                    lastName = u.ConsumerInfromation.LastName,
                    phoneNumber = u.PhoneNumber.GetPersianNumber(),
                    paidAmount = "HardCode",
                    lastVisit = "HardCode"
                }).Take(5).ToList();

            return new ResultDto<List<LatestConsumerForAdminDto>>()
            {
                Data = latestConsumers,
                IsSuccess = true
            };
        }
    }
}
