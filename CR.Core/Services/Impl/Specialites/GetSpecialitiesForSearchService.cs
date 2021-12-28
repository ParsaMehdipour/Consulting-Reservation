using System.Collections.Generic;
using System.Linq;
using CR.Core.Services.Interfaces.Specialites;
using CR.DataAccess.Context;

namespace CR.Core.Services.Impl.Specialites
{
    public class GetSpecialitiesForSearchService : IGetSpecialitiesForSearchService
    {
        private readonly ApplicationContext _context;

        public GetSpecialitiesForSearchService(ApplicationContext context)
        {
            _context = context;
        }

        public List<string> Execute()
        {
            return _context.Specialties.Select(s => s.Name).ToList();
        }
    }
}
