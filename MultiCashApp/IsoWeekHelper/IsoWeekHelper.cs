using System.Diagnostics.Contracts;
using System;
using System.Globalization;

namespace MultiCashApp.IsoWeekHelper
{
    public static class IsoWeekHelper
    {
        public static int GetIsoWeekNumber(DateTime date)
        {
            DayOfWeek day = CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(date);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                date = date.AddDays(3);
            }
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

    }
}
