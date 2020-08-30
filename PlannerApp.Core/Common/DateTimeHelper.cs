using System;

namespace PlannerApp.Shared.Common
{
    public class DateTimeHelper
    {
        /// <summary>
        /// Gets the most recent occurrence of the provided day of the week, relative to the specified date.
        /// </summary>
        /// <example>
        /// var date = new DateTime(2020, 9, 2, 8, 0, 0); // Wednesdayn September 2nd
        /// var target = DayOfWeek.Sunday;
        /// var result = DateTimeHelper.GetMostRecentDayOfWeek(date, target); // result is Sunday, August 30th
        /// </example>
        /// <param name="date">The date.</param>
        /// <param name="targetDayOfWeek">The target day of week.</param>
        /// <returns>The most recent date with the provided day of week. If date has that day of week, value is returned</returns>
        public static DateTime GetMostRecentDayOfWeek(DateTime date, DayOfWeek targetDayOfWeek)
        {
            var checkDate = date;
            while (checkDate.DayOfWeek != targetDayOfWeek)
            {
                checkDate = checkDate.AddDays(-1);
            }
            return checkDate;
        }

        public static double DaysBetween(DateTime startDate, DateTime endDate)
        {
            return startDate.Subtract(endDate).TotalDays;
        }

        /// <summary>
        /// Gets the hours as 12 hour time.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>The hour value (1 - 12)</returns>
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
        public static double CalculateLength(DateTimeOffset startDate, DateTimeOffset endDate) => (endDate - startDate).TotalMinutes;
        /// <summary>
        /// Format date as Month day, year
        /// e.g. June 27, 2020
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FormatFullDate(DateTime date) => date.ToString("MMMM d, yyyy");
        public static string FormatFullDate(DateTimeOffset date) => date.LocalDateTime.ToString("MMMM d, yyyy");
        /// <summary>
        /// Return user formatted time value (12 hour time) from date
        /// </summary>
        /// <param name="date"></param>
        /// <returns>12 hour time value</returns>
        public static string FormatTime(DateTime date) => date.ToString("h:mm tt");
        public static string FormatTime(DateTimeOffset dateOffset) => dateOffset.LocalDateTime.ToString("h:mm tt");
    }
}
