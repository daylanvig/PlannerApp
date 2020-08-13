using Application.PlannerItems.Queries.Common;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PlannerApp.UnitTests.Infrastructure;
using PresentationClient.Components;
using PresentationClient.Models;
using PresentationClient.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PlannerApp.UnitTests.ComponentTests
{
    public class CalendarBaseShould
    {
        private CalendarState state;
        private void SetupContext(TestContext ctx)
        {
            var testContextBuilder = new TestContextBuilder(ctx);
            testContextBuilder.AddCommon();
            var dataServiceMock = Mock.Of<IPlannerItemDataService>(o => o.LoadItems(It.IsAny<DateTime>(), It.IsAny<DateTime>()) == Task.FromResult<IEnumerable<PlannerItemModel>>(Array.Empty<PlannerItemModel>()));
            ctx.Services.AddScoped<IPlannerItemDataService>(o => dataServiceMock);
            state = new CalendarState();
            var calendarServiceMock = new Mock<ICalendarService>();
            calendarServiceMock.Setup(c => c.State).Returns(state);
            ctx.Services.AddScoped<ICalendarService>(o => calendarServiceMock.Object);
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
            Assert.Equal(new DateTime(2020, 5, 31, 15, 0, 0), state.Date.Value);
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
            Assert.Equal(new DateTime(2020, 5, 24, 15, 0, 0), state.Date.Value);
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
            Assert.Equal(new DateTime(2020, 6, 7, 15, 0, 0), state.Date.Value);
        }

        [Fact]
        public void LoadDataFrom0531To0606()
        {
            // Arrange
            using var ctx = new TestContext();
            new TestContextBuilder(ctx).AddCommon();
            var dataServiceMock = new Mock<IPlannerItemDataService>();
            dataServiceMock.Setup(o => o.LoadItems(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(Task.FromResult<IEnumerable<PlannerItemModel>>(Array.Empty<PlannerItemModel>()));
            ctx.Services.AddScoped<IPlannerItemDataService>(o => dataServiceMock.Object);
            var calendarServiceMock = new Mock<ICalendarService>();
            state = new CalendarState();
            calendarServiceMock.Setup(c => c.State).Returns(state);
            ctx.Services.AddScoped<ICalendarService>(o => calendarServiceMock.Object);

            // Act
            var cut = ctx.RenderComponent<CalendarBase>();

            // Assert
            dataServiceMock
                .Verify(o => o.LoadItems(new DateTime(2020, 5, 31, 15, 0, 0), new DateTime(2020, 6, 6, 15, 0, 0)), Times.Once);
        }
    }
}
