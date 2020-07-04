﻿using PlannerApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PlannerApp.Client.Components
{
    public static class UIComponentHelper
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

        /// <summary>
        /// Calculate luminance from hex string.
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns>Value of between 0 and 1, with 1 being meaning hex string was white</returns>
        private static float CalculateLuminance(string hexString)
        {
           
            if (hexString.IndexOf('#') != -1)
                hexString = hexString.Replace("#", "");

            int r, g, b;

            r = int.Parse(hexString.Substring(0, 2), NumberStyles.AllowHexSpecifier);
            g = int.Parse(hexString.Substring(2, 2), NumberStyles.AllowHexSpecifier);
            b = int.Parse(hexString.Substring(4, 2), NumberStyles.AllowHexSpecifier);

            return Color.FromArgb(r, g, b).GetBrightness();
        }

        public static string CalculateContrastingFontColour(string hexColourString)
        {
            if(CalculateLuminance(hexColourString) < 0.5)
            {
                return "has-text-grey";
            }
            return "has-text-white-ter";
        }
    }
}