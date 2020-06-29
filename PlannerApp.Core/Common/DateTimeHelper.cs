﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PlannerApp.Shared.Common
{
    public class DateTimeHelper
    {

        /// <summary>
        /// Format date as Month day, year
        /// e.g. June 27, 2020
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FormatFullDate(DateTime date) => date.ToString("MMMM d, yyyy");
        /// <summary>
        /// Return user formatted time value from date
        /// </summary>
        /// <param name="date"></param>
        /// <returns>12 hour time value</returns>
        public static string FormatTime(DateTime date) => date.ToString("h:mm tt");

        public static string FormatDateTimeLocalInput(DateTime? date) => date.HasValue ? date.Value.ToString("yyyy-MM-ddTHH:mm") : "";
    }
}
