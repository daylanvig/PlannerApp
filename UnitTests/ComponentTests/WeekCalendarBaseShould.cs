﻿using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PlannerApp.Client.Components;
using PlannerApp.Client.Services;
using PlannerApp.Shared.Models;
using PlannerApp.UnitTests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PlannerApp.UnitTests.ComponentTests
{
    public class WeekCalendarBaseShould
    {
        private void SetupContext(TestContext ctx)
        {
            var testContextBuilder = new TestContextBuilder(ctx);
            testContextBuilder.AddCommon();
            var dataServiceMock = Mock.Of<IPlannerItemDataService>(o => o.LoadItems(It.IsAny<DateTime>(), It.IsAny<DateTime>()) == Task.FromResult<IEnumerable<PlannerItemDTO>>(Array.Empty<PlannerItemDTO>()));
            ctx.Services.AddScoped<IPlannerItemDataService>(o => dataServiceMock);
        }

        [Fact]
        public void ShouldSetViewingDateToMostRecentSunday()
        {
            // Arrange
            // ! the context builder defaults to 2020, 6, 6
            using var ctx = new TestContext();
            SetupContext(ctx);

            // Act
            var cut = ctx.RenderComponent<CalendarBase>();

            // Assert
            Assert.Equal(new DateTime(2020, 5, 31, 15, 0, 0), cut.Instance.ViewingWeekOf);
        }

        [Fact]
        public void SetViewingDate7DaysEarlierOnRightSwipe()
        {
            // Arrange
            using var ctx = new TestContext();
            SetupContext(ctx);
            var cut = ctx.RenderComponent<CalendarBase>();

            // Act
            cut.Instance.HandleSwipe(UIComponents.Extensions.TouchSwipe.SwipeDirection.Right);

            // Assert
            Assert.Equal(new DateTime(2020, 5, 24, 15, 0, 0), cut.Instance.ViewingWeekOf);
        }

        [Fact]
        public void SetViewingDate7DaysLaterOnLeftSwipe()
        {
            // Arrange
            using var ctx = new TestContext();
            SetupContext(ctx);
            var cut = ctx.RenderComponent<CalendarBase>();

            // Act
            cut.Instance.HandleSwipe(UIComponents.Extensions.TouchSwipe.SwipeDirection.Left);

            // Assert
            Assert.Equal(new DateTime(2020, 6, 7, 15, 0, 0), cut.Instance.ViewingWeekOf);
        }

        [Fact]
        public void LoadDataFrom0531To0606()
        {
            // Arrange
            using var ctx = new TestContext();
            new TestContextBuilder(ctx).AddCommon();
            var dataServiceMock = new Mock<IPlannerItemDataService>();
            dataServiceMock.Setup(o => o.LoadItems(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(Task.FromResult<IEnumerable<PlannerItemDTO>>(Array.Empty<PlannerItemDTO>()));
            ctx.Services.AddScoped<IPlannerItemDataService>(o => dataServiceMock.Object);

            // Act
            var cut = ctx.RenderComponent<CalendarBase>();

            // Assert
            dataServiceMock
                .Verify(o => o.LoadItems(new DateTime(2020, 5, 31, 15, 0, 0), new DateTime(2020, 6, 6, 15, 0, 0)), Times.Once);
        }
    }
}
