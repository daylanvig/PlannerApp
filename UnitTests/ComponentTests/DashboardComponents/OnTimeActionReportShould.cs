using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PlannerApp.Client.Components.DashboardComponents;
using PlannerApp.Client.Services;
using PlannerApp.Shared.Models;
using PlannerApp.UnitTests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PlannerApp.UnitTests.ComponentTests.DashboardComponents
{
    public class OnTimeActionReportShould
    {
        private readonly TestContext ctx;
        private readonly Mock<IPlannerItemDataService> plannerItemDataServiceMock;
        private readonly List<PlannerItemDTO> items;
        public OnTimeActionReportShould()
        {
            var builder = new TestContextBuilder();
            items = new List<PlannerItemDTO>();
            plannerItemDataServiceMock = new Mock<IPlannerItemDataService>();
            plannerItemDataServiceMock.Setup(o => o.LoadOverdueItems()).ReturnsAsync(items);
            ctx = builder.Build();
            ctx.Services.AddScoped<IPlannerItemDataService>(o => plannerItemDataServiceMock.Object);
        }

        [Fact]
        public void LoadOverdueItems()
        {
            ctx.RenderComponent<OnTimeActionReportBase>();
            plannerItemDataServiceMock.Verify(o => o.LoadOverdueItems(), Times.Once);
        }
    }
}
