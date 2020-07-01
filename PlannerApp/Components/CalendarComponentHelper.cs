using PlannerApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Client.Components
{
    public static class CalendarComponentHelper
    {
        private const int ROWHEIGHT = 80; // px
        /// <summary>
        /// Calculate height in px
        /// </summary>
        /// <returns>px value for css height</returns>
        public static string CalculateHeight(DateTime startDate, DateTime endDate)
        {
            var fractionsOfHour = DateTimeHelper.CalculateLength(startDate, endDate) / 60;

            return $"{ROWHEIGHT * fractionsOfHour}px";
        }

        /// <summary>
        /// Get position to set top
        /// </summary>
        /// <returns>px value</returns>
        public static string CalculateTop(DateTime startDate)
        {
            var startMinuteFractions = (double)startDate.Minute / 60;
            return $"{ROWHEIGHT * startMinuteFractions}px";
        }
    }
}
