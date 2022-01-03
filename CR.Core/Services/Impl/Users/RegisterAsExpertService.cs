using System;
using CR.Core.Services.Interfaces.Users;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.IndividualInformations;
using CR.DataAccess.Enums;
using System.Linq;

namespace CR.Core.Services.Impl.Users
{
    public class RegisterAsExpertService : IRegisterAsExpertService
    {
        private readonly ApplicationContext _context;

        public RegisterAsExpertService(ApplicationContext context)
        {
            _context = context;
        }

        public void Execute(long userId,string name,string lastName)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == userId);

                user.UserFlag = UserFlag.Expert;
                user.IsActive = false;

                var expertInformation = new ExpertInformation()
                {
                    FirstName = name,
                    LastName = lastName,
                    Expert = user,
                    ExpertId = user.Id
                };

                _context.ExpertInformations.Add(expertInformation);

                _context.SaveChanges();

                user.ExpertInformationId = expertInformation.Id;

                _context.SaveChanges();

                transaction.Commit();
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
