using System;
using System.Collections.Generic;
using System.Linq;

namespace CR.Common.Utilities
{
    public static class LinqHelper
    {
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> data)
        {
            List<T> listResult = new List<T>();
            List<T> lList = data.ToList();
            Random lRandom = new();

            while (lList.Count > 0)
            {
                var lintPos = lRandom.Next(lList.Count);
                listResult.Add(lList[lintPos]);
                lList.RemoveAt(lintPos);
            }

            return listResult;
        }
    }
}
