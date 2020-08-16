using AutoFixture;
using Shared.TestSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Application.Categories.Queries.Common;
using Domain.Categories;

namespace PresentationServer.Categories
{
    public class CategoriesControllerShould : IClassFixture<CategoriesControllerTestFixture>
    {
        private CategoriesController sut;
        private readonly CategoriesControllerTestFixture testFixture;

        public CategoriesControllerShould(CategoriesControllerTestFixture testFixture)
        {
            this.testFixture = testFixture;
            sut = testFixture.CreateSUT();
        }

        [Fact]
        public void CallListAllCatgoriesFromGetCategories()
        {
            // Arrange
            testFixture.categoryRepositoryMock.ResetCalls();
            // Act
            var _ = sut.GetCategories().Result;
            // Assert
            testFixture.categoryRepositoryMock.Verify(m => m.ListAllAsync(), Times.Once);
        }

        [Fact]
        public void ReturnCatgoriesOrderedByDescriptionFromGetCategories()
        {
            // Arrange
            var orderedNames = testFixture.categories.Select(c => c.Description).OrderBy(d => d);
            // Act
            var categories = sut.GetCategories().Result.Value;
            // Assert
            Assert.True(orderedNames.SequenceEqual(categories.Select(c => c.Description)));
        }

        [Fact]
        public void ReturnNotFoundWhenCategoryRepositoryReturnsNull()
        {
            var result = sut.GetCategory(testFixture.INVALID_ID).Result;
            Assert.IsType<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
        }

        [Fact]
        public void ReturnCategoryModelWhenCategoryRepositoryDoesNotReturnNull()
        {
            var result = sut.GetCategory(testFixture.ID).Result;
            Assert.NotNull(result.Value);
        }

        [Fact]
        public void CallAddAsyncOneTime()
        {
            // Arrange
            testFixture.categoryRepositoryMock.ResetCalls();
            // Act
            var _ = sut.AddCategory(testFixture.fixture.Create<CategoryModel>()).Result;
            // Assert
            testFixture.categoryRepositoryMock
                .Verify(m => m.AddAsync(It.IsAny<Category>()), Times.Once);
        }

        [Fact]
        public void ReturnCreatedAtActionResult()
        {
            var result = sut.AddCategory(testFixture.fixture.Create<CategoryModel>()).Result;
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.NotNull(createdAtActionResult.Value);
        }
    }
}
