using Application.Interfaces.Persistence;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMoq;
using Domain.Categories;
using Shared.TestSupport;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Application.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryShould
    {
        private const string DESCRIPTION = "Description 1";
        private const int ID = 1;
        private const string COLOUR = "#187995";

        private readonly Category category;
        private GetAllCategoriesQuery sut;
        private readonly AutoMoqer mocker;
        private readonly Fixture fixture;
        public GetAllCategoriesQueryShould()
        {
            fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            fixture.Customize<Category>(m => m.Without(c => c.PlannerItems));
            category = new Category
            {
                ID = ID,
                Description = DESCRIPTION,
                Colour = COLOUR
            };
            mocker = new AutoMoqer();
            var categoryQueryableMock = new List<Category> { category }.AsAsyncQueryable();
            var categoryRepository = mocker
                .GetMock<ICategoryRepository>()
                .Setup(r => r.GetAll())
                .Returns(categoryQueryableMock);
            sut = mocker.Create<GetAllCategoriesQuery>();
        }

        [Fact]
        public void ReturnAllCategories()
        {
            var results = sut.Execute().Result;
            Assert.Single(results);
        }

        [Fact]
        public void IncludesAllCategoryModelAttributes()
        {
            var result = sut.Execute().Result.First();
            Assert.Equal(ID, result.ID);
            Assert.Equal(COLOUR, result.Colour);
            Assert.Equal(DESCRIPTION, result.Description);
        }

        [Fact]
        public void OrderCategoriesByDescription()
        {
            // Arrange
            var categories = new List<Category>();
            fixture.AddManyTo(categories);
            mocker.GetMock<ICategoryRepository>().Setup(c => c.GetAll()).Returns(categories.AsAsyncQueryable());
            sut = mocker.Create<GetAllCategoriesQuery>();
            // Act
            var results = (sut.Execute().Result).ToList();
            var orderedResults = results.OrderBy(r => r.Description);
            Assert.Equal(orderedResults, results);
        }
    }
}
