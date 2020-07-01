using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PlannerApp.Shared.Common
{
    public class DateTimeHelper
    {
        public static DateTime GetMostRecentDayOfWeek(DateTime date, DayOfWeek targetDayOfWeek)
        {
            var checkDate = date;
            while (checkDate.DayOfWeek != targetDayOfWeek)
            {
                checkDate = checkDate.AddDays(-1);
            }
            return checkDate;
        }

        public static string GetHoursAs12HourTime(DateTime date)
        {
            var hours = date.Hour;
            if (hours > 12)
            {
                hours -= 12;
            }
            else if (hours == 0)
            {   // show midnight as 12
                hours += 12;
            }
            return hours.ToString();
        }

        /// <summary>
        /// Calculate number of minutes between two dates.
        /// Does not include seconds.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static double CalculateLength(DateTime startDate, DateTime endDate) => (endDate - startDate).TotalMinutes;
        /// <summary>
        /// Format date as Month day, year
        /// e.g. June 27, 2020
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FormatFullDate(DateTime date) => date.ToString("MMMM d, yyyy");
        /// <summary>
        /// Return user formatted time value (12 hour time) from date
        /// </summary>
        /// <param name="date"></param>
        /// <returns>12 hour time value</returns>
        public static string FormatTime(DateTime date) => date.ToString("h:mm tt");

        public static string FormatDateTimeLocalInput(DateTime? date) => date.HasValue ? date.Value.ToString("yyyy-MM-ddTHH:mm") : "";
        public static string FormatDateInput(DateTime? date) => date == null ? "" : date.Value.ToString("yyyy-MM-dd");
    }
}
