using CR.Common.DTOs;
using CR.Core.Services.Interfaces.Factors;
using CR.DataAccess.Context;
using System.Linq;

namespace CR.Core.Services.Impl.Factors
{
    public class UpdateFactorCartHolderPanService : IUpdateFactorCartHolderPanService
    {
        private readonly ApplicationContext _context;

        public UpdateFactorCartHolderPanService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(string factorNumber, string cardHolderPAN)
        {
            var factor = _context.Factors.FirstOrDefault(_ => _.FactorNumber == factorNumber);

            if (factor == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = string.Empty
                };
            }

            factor.CardHolderPAN = cardHolderPAN;

            _context.SaveChanges();

            return new ResultDto()
            {
                IsSuccess = true,
                Message = string.Empty
            };
        }
    }
}
