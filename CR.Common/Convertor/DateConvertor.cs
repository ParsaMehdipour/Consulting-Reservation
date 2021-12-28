using System;
using System.Collections.Generic;
using System.Globalization;

namespace CR.Common.Convertor
{
    public static class DateConvertor
    {
        public static string GetDayOfWeek(DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime dt = date;
            string day = pc.GetDayOfWeek(dt).ToString();

            switch (day)
            {
                case "Saturday":
                    return "شنبه";
                case "Sunday":
                    return "یک شنبه";
                case "Monday":
                    return "دو شنبه";
                case "Tuesday":
                    return "سه شنبه";
                case "Wednesday":
                    return "چهار شنبه";
                case "Thursday":
                    return "پنجشنبه";
                case "Friday":
                    return "جمعه";
                default:
                    return "";

            }

        }

        public static int GetAge(this DateTime dob)
        {
            int age = 0;
            age = DateTime.Now.Subtract(dob).Days;
            age = age / 365;
            return age;
        }

        public static string ToShamsi(this DateTime dateTime)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(dateTime) + "/" + pc.GetMonth(dateTime).ToString("00") + "/" + pc.GetDayOfMonth(dateTime).ToString("00");
        }

        private static readonly string[] Pn = { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };
        private static readonly string[] En = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        public static string ToEnglishNumber(this string strNum)
        {
            var cash = strNum;
            for (var i = 0; i < 10; i++)
                cash = cash.Replace(Pn[i], En[i]);
            return cash;
        }

        public static DateTime ToGeorgianDateTime(this string persianDate)
        {
            persianDate = persianDate.ToEnglishNumber();
            var year = Convert.ToInt32(persianDate.Substring(0, 4));
            var month = Convert.ToInt32(persianDate.Substring(5, 2));
            var day = Convert.ToInt32(persianDate.Substring(8, 2));
            return new DateTime(year, month, day, new PersianCalendar());
        }



    }
}
