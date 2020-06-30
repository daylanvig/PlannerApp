using System;
using System.Collections.Generic;
using System.Text;

namespace PlannerApp.UnitTests.Data
{
    public class DateTimeHelperTestData
    {
        public static IEnumerable<object[]> CalculateLengthData => new List<object[]>
        {
            new object[] { new DateTime(2020, 1, 1, 8, 0, 0), new DateTime(2020, 1, 1, 8, 0, 0), 0 }, // equal
            new object[] { new DateTime(2020, 1, 1, 8, 0, 0), new DateTime(2020, 1, 1, 7, 30, 0), -30 }, // past
            new object[] { new DateTime(2020, 1, 1, 8, 0, 0), new DateTime(2020, 1, 1, 8, 0, 30), 0.5 }, 
            new object[] { new DateTime(2020, 1, 1, 8, 0, 0), new DateTime(2020, 1, 1, 9, 30, 0),  90}, // future
        };
    }
}
