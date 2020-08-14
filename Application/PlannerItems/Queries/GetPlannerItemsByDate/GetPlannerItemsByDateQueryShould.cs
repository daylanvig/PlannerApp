using Application.Interfaces.Persistence;
using Application.PlannerItems.Common;
using Application.PlannerItems.Queries.Common;
using AutoFixture;
using AutoMapper;
using Domain.PlannerItems;
using Moq;
using Shared.TestSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Application.PlannerItems.Queries.GetPlannerItemsByDate
{
    public class GetPlannerItemsByDateQueryShould : IClassFixture<PlannerItemsTestFixture>
    {
        private readonly GetPlannerItemsByDateQuery sut;
        private readonly PlannerItemsTestFixture fixture;

        private readonly DateTime STARTDATE = new DateTime(2019, 2, 2, 8, 0, 0);
        private readonly DateTime ENDDATE = new DateTime(2019, 2, 12, 8, 0, 0);
        public GetPlannerItemsByDateQueryShould(PlannerItemsTestFixture fixture)
        {
            this.fixture = fixture;
            sut = fixture.Create<GetPlannerItemsByDateQuery>();
        }

        [Fact]
        public void ReturnAllItems() {

            var result = sut.Execute(null, null).Result;
            Assert.Equal(fixture.Items.Count, result.Count);
        }

        [Fact]
        public void ReturnItemsForSingleDate()
        {
            // Arrange
            var item = fixture.Create<PlannerItem>();
            item.PlannedActionDate = STARTDATE;
            fixture.Items.Add(item);
            // Act
            var result = sut.Execute(STARTDATE, null).Result;
            // Assert
            Assert.Single(result);
            Assert.Equal(item.ID, result.First().ID);
        }

        [Fact]
        public void ReturnItemsWithinDateRange()
        {
            // Arrange
            fixture.Items[0].PlannedActionDate = STARTDATE;
            fixture.Items[1].PlannedActionDate = STARTDATE.AddDays(2);
            fixture.Items[2].PlannedActionDate = STARTDATE.AddDays(-1);
            fixture.Items[3].PlannedActionDate = ENDDATE;
            for(var i = 4; i < fixture.Items.Count(); i++)
            {
                fixture.Items[i].PlannedActionDate = ENDDATE.AddDays(1);
            }
            var validIDs = new List<int>()
            {
                fixture.Items[0].ID,
                fixture.Items[1].ID,
                fixture.Items[3].ID,
            };
            // Act
            var result = sut.Execute(STARTDATE, ENDDATE).Result;
            // Assert
            Assert.Equal(validIDs, result.Select(r => r.ID));
            Assert.Equal(3, result.Count());
        }
    }
}
