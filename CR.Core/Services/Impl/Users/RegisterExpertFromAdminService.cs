using System;
using System.Linq;
using System.Threading.Tasks;
using CR.Common.DTOs;
using CR.Core.DTOs.Users;
using CR.Core.Services.Interfaces.Users;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.IndividualInformations;
using CR.DataAccess.Entities.Users;
using CR.DataAccess.Enums;
using Microsoft.AspNetCore.Identity;

namespace CR.Core.Services.Impl.Users
{
    public class RegisterExpertFromAdminService : IRegisterExpertFromAdminService
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;

        public RegisterExpertFromAdminService(ApplicationContext context
        ,UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ResultDto> Execute(RegisterExpertFromAdminDto request)
        {
            await using var transaction = _context.Database.BeginTransaction();

            try
            {
                var user = new User
                {
                    UserName = request.phoneNumber,
                    PhoneNumber = request.phoneNumber,
                    Email = request.email,
                    UserFlag = UserFlag.Expert,
                    IsActive = false
                };

                await _userManager.CreateAsync(user, request.password);

                var addedUser = _context.Users.FirstOrDefault(u => u.UserName == user.UserName);

                var expertInformation = new ExpertInformation()
                {
                    Gender = (request.gender == 0) ? GenderType.Male : GenderType.Female,
                    FirstName = request.firstName,
                    LastName = request.lastName,
                    Province = request.province,
                    City = request.city,
                    SpecificAddress = request.specificAddress,
                    PostalCode = request.postalCode,
                    ExpertId = addedUser.Id,
                    Expert = addedUser,
                    IsFreeOfCharge = true
                };


                _context.ExpertInformations.Add(expertInformation);
                _context.SaveChanges();

                user.ExpertInformationId = expertInformation.Id;
                user.ExpertInformation = expertInformation;

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "متخصص با موفقیت افزوده شد"
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
