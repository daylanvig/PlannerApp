using Application.PlannerItems.Commands.Shared;
using Application.PlannerItems.Common;
using AutoFixture;
using Xunit;
using Moq;
using Domain.PlannerItems;
using System;

namespace Application.PlannerItems.Commands.EditPlannerItem
{
    public class EditPlannerItemCommandShould
    {
        private EditPlannerItemCommand sut;
        private readonly PlannerItemsTestFixture fixture;

        private const int ID = 1;
        private readonly PlannerItemCreateEditModel editModel;

        public EditPlannerItemCommandShould()
        {
            this.fixture = new PlannerItemsTestFixture();
            sut = fixture.Create<EditPlannerItemCommand>();
            editModel = fixture.Create<PlannerItemCreateEditModel>();
        }

        [Fact]
        public void LoadFromRepositoryUsingID()
        {
            var _ = sut.Execute(ID, editModel).Result;
            fixture.repositoryMock.Verify(m => m.GetByIdAsync(ID), Times.Once);
        }

        [Fact]
        public void InvokeUpdateAsync()
        {
            var _ = sut.Execute(ID, editModel).Result;
            fixture.repositoryMock.Verify(m => m.UpdateAsync(It.IsAny<PlannerItem>()), Times.Once);
        }
        
        [Fact]
        public void ThrowArgumentExceptionIfItemNotFoundByRepo()
        {
            // Arrange
            fixture.repositoryMock
                .Setup(m => m.GetByIdAsync(ID))
                .ReturnsAsync((PlannerItem)null);
            // Act
            sut = fixture.Create<EditPlannerItemCommand>();
            // Assert
            Assert.ThrowsAsync<ArgumentException>(() => sut.Execute(ID, editModel));
        }
    }
}
