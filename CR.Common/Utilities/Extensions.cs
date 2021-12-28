using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace CR.Common.Utilities
{
    public static class Extensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                ?.GetName();
        }

        public static string GetPersianNumber(this string englishNumber)
        {
            if (!string.IsNullOrWhiteSpace(englishNumber))
            {
                string persianNumber = "";
                foreach (char ch in englishNumber)
                {
                    persianNumber += (char)(1776 + char.GetNumericValue(ch));
                }
                return persianNumber;
            }
            else
            {
                return "";
            }
        }
    }
}
