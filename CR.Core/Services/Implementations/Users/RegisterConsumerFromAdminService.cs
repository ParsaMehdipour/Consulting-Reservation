using CR.Common.DTOs;
using CR.Core.DTOs.Users;
using CR.Core.Services.Interfaces.Users;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.IndividualInformations;
using CR.DataAccess.Entities.Users;
using CR.DataAccess.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CR.Core.Services.Implementations.Users
{
    public class RegisterConsumerFromAdminService : IRegisterConsumerFromAdminService
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;

        public RegisterConsumerFromAdminService(ApplicationContext context
        , UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ResultDto> Execute(RegisterConsumerFromAdminDto request)
        {
            await using var transaction = _context.Database.BeginTransaction();

            try
            {
                var user = new User
                {
                    UserName = request.phoneNumber,
                    PhoneNumber = request.phoneNumber,
                    Email = request.email,
                    UserFlag = UserFlag.Consumer,
                    IsActive = false
                };

                await _userManager.CreateAsync(user, request.password);

                var addedUser = _context.Users.FirstOrDefault(u => u.UserName == user.UserName);

                var consumerInformation = new ConsumerInfromation()
                {
                    Gender = request.gender,
                    FirstName = request.firstName,
                    LastName = request.lastName,
                    Province = request.province,
                    City = request.city,
                    SpecificAddress = request.specificAddress,
                    Degree = request.degree,
                    ConsumerId = addedUser.Id,
                    Consumer = addedUser
                };


                _context.ConsumerInfromations.Add(consumerInformation);
                _context.SaveChanges();

                user.ConsumerInformationId = consumerInformation.Id;
                user.ConsumerInfromation = consumerInformation;

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "مراجعه کننده با موفقیت افزوده شد"
                };

            }
            catch (Exception)
            {
                transaction.Rollback();

                throw;
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
