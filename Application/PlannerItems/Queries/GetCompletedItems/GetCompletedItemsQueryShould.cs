using Application.PlannerItems.Common;
using AutoFixture;
using System;
using System.Linq;
using Xunit;

namespace Application.PlannerItems.Queries.GetCompletedItems
{
    public class GetCompletedItemsQueryShould : IClassFixture<PlannerItemsTestFixture>
    {
        private readonly GetCompletedItemsQuery sut;
        private readonly PlannerItemsTestFixture fixture;
        public GetCompletedItemsQueryShould(PlannerItemsTestFixture fixture)
        {
            this.fixture = fixture;
            fixture.Items.ForEach(item => item.CompletionDate = null);
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
            fixture.Items[0].CompletionDate = fixture.Create<DateTime>();
            // Act
            var results = sut.Execute().Result;
            // Assert
            Assert.Single(results);
            Assert.Equal(fixture.Items[0].ID, results.First().ID);
        }
    }
}
