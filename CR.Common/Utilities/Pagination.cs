using System.Collections.Generic;
using System.Linq;

namespace CR.Common.Utilities
{
    public static class Pagination
    {

        public static IEnumerable<TSource> ToPaged<TSource>(this IEnumerable<TSource> source, int page, int pageSize, out int rowsCount)
        {
            if (source==null)
            {
                rowsCount = 0;
                return null;
            }
            rowsCount = source.Count();
            var result=source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return result;
        }
    }
}
