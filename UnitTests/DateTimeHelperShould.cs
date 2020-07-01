using PlannerApp.Shared.Common;
using PlannerApp.UnitTests.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PlannerApp.UnitTests
{
    public class DateTimeHelperShould
    {
        [Fact]
        public void Return830AM()
        {
            var date = new DateTime(2020, 10, 1, 8, 30, 0);
            Assert.Equal("8:30 AM", DateTimeHelper.FormatTime(date));
        }

        [Fact]
        public void ReturnJune272020()
        {
            var date = new DateTime(2020, 6, 27, 0, 0, 0);
            Assert.Equal("June 27, 2020", DateTimeHelper.FormatFullDate(date));
        }

        [Fact]
        public void ReturnProvidedSundayDate()
        {
            var date = new DateTime(2020, 6, 28, 0, 0, 0);
            Assert.Equal(date.Date, DateTimeHelper.GetMostRecentDayOfWeek(date, DayOfWeek.Sunday));
        }

        [Fact]
        public void ReturnDec302019()
        {
            var date = new DateTime(2020, 1, 3, 0, 0, 0);
            Assert.Equal(new DateTime(2019, 12, 30, 0, 0, 0), DateTimeHelper.GetMostRecentDayOfWeek(date, DayOfWeek.Monday));
        }

        [Theory]
        [MemberData(nameof(DateTimeHelperTestData.CalculateLengthData), MemberType = typeof(DateTimeHelperTestData))]
        public void CalculateLengthValues(DateTime startDate, DateTime endDate, double expected)
        {
            Assert.Equal(expected, DateTimeHelper.CalculateLength(startDate, endDate));
        }
    }
}
