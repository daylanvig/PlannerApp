using Application.PlannerItems.Queries.Common;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PlannerApp.UnitTests.Infrastructure;
using PresentationClient.Components.DashboardComponents;
using PresentationClient.Services;
using PresentationClient.Services.ComponentHelperServices;
using System.Collections.Generic;
using Xunit;

namespace PlannerApp.UnitTests.ComponentTests.DashboardComponents
{
    public class OnTimeActionReportShould
    {
        private readonly TestContext ctx;
        private readonly Mock<IPlannerItemDataService> plannerItemDataServiceMock;
        private readonly List<PlannerItemModel> items;
        public OnTimeActionReportShould()
        {
            var builder = new TestContextBuilder();
            builder.AddDateProvider();
            items = new List<PlannerItemModel>();
            plannerItemDataServiceMock = new Mock<IPlannerItemDataService>();
            plannerItemDataServiceMock.Setup(o => o.LoadOverdueItems()).ReturnsAsync(items);
            ctx = builder.Build();
            ctx.Services.AddScoped<IPlannerItemComponentService>(o => Mock.Of<IPlannerItemComponentService>());
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
