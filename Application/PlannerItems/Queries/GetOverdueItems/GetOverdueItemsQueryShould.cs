using Application.PlannerItems.Common;
using AutoFixture;
using Moq;
using PlannerApp.Shared.Common;
using Shared.TestSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Application.PlannerItems.Queries.GetOverdueItems
{
    public class GetOverdueItemsQueryShould : IClassFixture<PlannerItemsTestFixture>
    {
        private readonly GetOverdueItemsQuery sut;
        private readonly PlannerItemsTestFixture fixture;

        private readonly DateTime DATE = new DateTime(2019, 6, 6, 8, 0, 0);

        public GetOverdueItemsQueryShould(PlannerItemsTestFixture fixture)
        {
            this.fixture = fixture;
            fixture.Items.ForEach(item =>
            {
                item.CompletionDate = null;
                item.PlannedEndTime = DATE.AddDays(1);
            });
            fixture.Freeze<Mock<IDateTimeProvider>>()
                .Setup(m => m.Now)
                .Returns(DATE);

            sut = fixture.Create<GetOverdueItemsQuery>();
        }

        [Fact]
        public void ReturnNoItems()
        {
            Assert.Empty(sut.Execute().Result);
        }

        [Fact]
        public void ReturnItemWithPlannedEndTimeBeforeCurrentTime()
        {
            // Arrange
            fixture.Items[0].PlannedEndTime = DATE.AddDays(-1);
            // Act
            var results = sut.Execute().Result;
            // Assert
            Assert.Single(results);
            Assert.Equal(fixture.Items[0].ID, results.First().ID);
        }

        [Fact]
        public void NotReturnItemWithCompletionDateSet()
        {
            // Arrange
            fixture.Items[0].PlannedEndTime = DATE.AddDays(-1);
            fixture.Items[0].CompletionDate = DATE;
            // Act
            var results = sut.Execute().Result;
            // Assert
            Assert.Empty(results);
        }
    }
}
