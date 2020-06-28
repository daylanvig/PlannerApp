using PlannerApp.Shared.Common;
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
    }
}
