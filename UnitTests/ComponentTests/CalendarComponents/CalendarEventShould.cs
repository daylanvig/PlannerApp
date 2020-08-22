using Application.Categories.Queries.Common;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PlannerApp.UnitTests.Infrastructure;
using ClientApp.Components.CalendarComponents;
using ClientApp.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PlannerApp.UnitTests.ComponentTests.CalendarComponents
{
    public class CalendarEventShould : IDisposable
    {
        private readonly CategoryModel category;
        private readonly TestContext ctx;
        private readonly PlannerItemModelBuilder itemBuilder;
        public CalendarEventShould()
        {
            category = new CategoryModel
            {
                ID = 2,
                Colour = "#03e8fc",
                Description = "TestCategory"
            };
            ctx = new TestContext();
            var categoryMock = Mock.Of<ICategoryDataService>(m => m.LoadCategory(2) == Task.FromResult(category));
            ctx.Services.AddScoped<ICategoryDataService>(o => categoryMock);
            itemBuilder = new PlannerItemModelBuilder();
        }

        public void Dispose()
        {
            ctx.Dispose();
        }

        [Fact]
        public void RenderCorrectly()
        {
            // Arrange
            var item = itemBuilder.Build();

            // Act
            var cut = ctx.RenderComponent<CalendarEvent>(o =>
            {
                o.Add(p => p.Item, item);
            });

            // Assert
            cut.MarkupMatches(@"<div class='calendar__event has-text-white-ter' style='height: 80px; top: 560px; background-color: #b2bec3'>
                                    <div class='calendar__event-description'>Test</div>
                                    <div class='planner__event-time'>7:00 AM - 8:00 AM</div>
                                </div>");
        }

        [Fact]
        public void UseItemsCategoryColour()
        {
            // Arrange
            var item = itemBuilder.WithCategory(2).Build();
            // Act
            var cut = ctx.RenderComponent<CalendarEvent>(o =>
            {
                o.Add(p => p.Item, item);
            });
            // Assert
            Assert.Contains("background-color: #03e8fc", cut.Markup);
        }
    }
}
