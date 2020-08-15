using Application.PlannerItems.Commands.CreatePlannerItem;
using Application.PlannerItems.Commands.EditPlannerItem;
using Application.PlannerItems.Commands.Shared;
using Application.PlannerItems.Queries.Common;
using Application.PlannerItems.Queries.GetCompletedItems;
using Application.PlannerItems.Queries.GetPlannerItemsByDate;
using AutoFixture;
using Domain.PlannerItems;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shared.TestSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PresentationServer.PlannerItems
{
    public class PlannerItemsControllerShould : IClassFixture<PlannerItemsControllerTestFixture>
    {
        private PlannerItemsController sut;
        private readonly PlannerItemsControllerTestFixture fixture;

        private readonly DateTime DATE;
        public PlannerItemsControllerShould(PlannerItemsControllerTestFixture testFixture)
        {
            fixture = testFixture;
            DATE = fixture.fixture.Create<DateTime>();
            sut = fixture.CreateSUT();
        }

        [Fact]
        public void ReturnListOfPlannerItemModelsFromGetItems()
        {
            var result = sut.GetItems(DATE, DATE).Result;
            Assert.Equal(fixture.itemModel, result.Single());
        }

        [Fact]
        public void ExecutesGetPlannerItemsByDateQueryFromGetItems()
        {
            // Arrange
            fixture.getPlannerItemsByDateQueryMock.ResetCalls();
            // Act
            var result = sut.GetItems(DATE, DATE).Result;
            // Assert
            fixture.getPlannerItemsByDateQueryMock.Verify(m => m.Execute(DATE, DATE), Times.Once);
        }

        [Fact]
        public void ReturnListOfPlannerItemModelsFromGetCompletedItems()
        {
            var result = sut.GetCompletedItems().Result;
            Assert.Equal(fixture.itemModel, result.Single());
        }

        [Fact]
        public void ExecutesGetCompletedItemsFromGetCompletedItems()
        {
            // Arrange
            fixture.getCompletedItemsQueryMock.ResetCalls();
            // Act
            var result = sut.GetCompletedItems();
            // Assert
            fixture.getCompletedItemsQueryMock.Verify(m => m.Execute(), Times.Once);
        }

        [Fact]
        public void ReturnListOfPlannerItemModelsFromGetOverdueItems()
        {
            var result = sut.GetOverdueItems().Result;
            Assert.Equal(fixture.itemModel, result.Single());
        }

        [Fact]
        public void ExecutesGetOverdueItemsFromGetOverdueItems()
        {
            // Arrange
            fixture.getOverdueItemsQueryMock.ResetCalls();
            // Act
            var result = sut.GetOverdueItems();
            // Assert
            fixture.getOverdueItemsQueryMock.Verify(m => m.Execute(), Times.Once);
        }

        [Fact]
        public void LoadFromRepositoryInGetByID()
        {
            // Arrange
            fixture.plannerItemRepositoryMock.ResetCalls();
            fixture.plannerItemRepositoryMock
                .Setup(m => m.GetByIdAsync(1))
                .ReturnsAsync(fixture.fixture.Create<PlannerItem>());
            // Act
            var result = fixture.CreateSUT().GetByID(1).Result;
            // Assert
            fixture.plannerItemRepositoryMock.Verify(m => m.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public void ReturnMappedModelFromGetByID()
        {
            Assert.Equal(fixture.createEditModel, sut.GetByID(1).Result);
        }

        [Fact]
        public void InvokeCreatePlannerItemCommandInAddNewItem()
        {
            // Arrange
            fixture.createPlannerItemCommandMock.ResetCalls();
            // Act
            var _ = sut.AddNewItem(fixture.createEditModel).Result;
            // Assert
            fixture.createPlannerItemCommandMock.Verify(m => m.Execute(fixture.createEditModel), Times.Once);
        }

        [Fact]
        public void ReturnAPlannerItemModelFromAddNewItem()
        {
            var result = sut.AddNewItem(fixture.createEditModel).Result;
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            var itemResult = Assert.IsType<PlannerItemModel>(actionResult.Value);
            Assert.Equal(fixture.itemModel.ID, itemResult.ID);
        }

        [Fact]
        public void ReturnAPlannerItemModelFromEditItem()
        {
            // Arrange
            fixture.editPlannerItemCommandMock
               .Setup(m => m.Execute(It.IsAny<int>(), fixture.createEditModel))
               .ReturnsAsync(fixture.itemModel);
            // Act
            var result = sut.EditItem(fixture.createEditModel.ID, fixture.createEditModel).Result;
            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var plannerItemModel = Assert.IsType<PlannerItemModel>(actionResult.Value);
            Assert.Equal(fixture.itemModel.ID, plannerItemModel.ID);
        }

        [Fact]
        public void ReturnsBadRequestResultFromEditItemEndpointWhenCommandThrowsArgumentException()
        {
            // Arrange
            fixture.editPlannerItemCommandMock
                   .Setup(m => m.Execute(It.IsAny<int>(), fixture.createEditModel))
                   .ThrowsAsync(new ArgumentException());
            sut = fixture.CreateSUT();
            // Act
            var result = sut.EditItem(fixture.itemModel.ID, fixture.createEditModel).Result;
            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void ReturnBadRequestFromDeleteEndpointIfRepositoryReturnsNull()
        {
            // Arrange
            fixture.plannerItemRepositoryMock
                .Setup(m => m.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((PlannerItem)null);
            sut = fixture.CreateSUT();
            // Act
            var result = sut.DeletePlannerItem(1).Result;
            Assert.IsType<BadRequestResult>(result);
        }
        
        [Fact]
        public void InvokeDeleteAsyncFromDeleteEndpoint()
        {
            // Arrange
            var item = fixture.fixture.Create<PlannerItem>();
            fixture.plannerItemRepositoryMock.ResetCalls();
            fixture.plannerItemRepositoryMock
                .Setup(m => m.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(item);
            sut = fixture.CreateSUT();
            // Act
            var result = sut.DeletePlannerItem(1).Result;
            fixture.plannerItemRepositoryMock.Verify(mocks => mocks.DeleteAsync(item), Times.Once);
        }
    }
}
