using Application.Interfaces.Persistence;
using Application.PlannerItems.Common;
using AutoFixture;
using Domain.PlannerItems;
using Moq;
using Shared.TestSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Application.PlannerItems.Queries.GetCompletedItems
{
    public class GetCompletedItemsQueryShould : IClassFixture<PlannerItemsTestFixture>
    {
        private readonly GetCompletedItemsQuery sut;
        private readonly Fixture fixture;
        private readonly List<PlannerItem> items = new List<PlannerItem>();
        public GetCompletedItemsQueryShould(PlannerItemsTestFixture fixture)
        {
            this.fixture = fixture;
            fixture.AddManyTo(items, 10);
            fixture.Freeze<Mock<IPlannerItemRepository>>()
                .Setup(p => p.GetAll())
                .Returns(() => items.AsAsyncQueryable());
            items.ForEach(item => item.CompletionDate = null);
            sut = fixture.Create<GetCompletedItemsQuery>();
        }

        [Fact]
        public void ReturnNoItems()
        {
            var results = sut.Execute().Result;
            Assert.Empty(results);
        }

        [Fact]
        public void ReturnOneCompletedItem()
        {
            // Arrange
            items[0].CompletionDate = fixture.Create<DateTime>();
            // Act
            var results = sut.Execute().Result;
            // Assert
            Assert.Single(results);
            Assert.Equal(items[0].ID, results.First().ID);
        }
    }
}
