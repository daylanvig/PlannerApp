using Application.Categories.Common;
using Application.Interfaces.Persistence;
using AutoFixture;
using AutoMapper;
using Domain.Categories;
using Moq;
using Shared.TestSupport;
using System.Collections.Generic;
using System.Linq;

namespace PresentationServer.Categories
{
    public class CategoriesControllerTestFixture
    {
        public readonly Fixture fixture;
        public readonly Mock<ICategoryRepository> categoryRepositoryMock;

        public readonly List<Category> categories = new List<Category>();
        public readonly int ID = 2;
        public readonly int INVALID_ID = 5;

        public CategoriesControllerTestFixture()
        {
            fixture = TestFixture.Create();
            fixture.AddManyTo(categories, 10);
            var cfg = new MapperConfiguration(m => m.AddProfile(new CategoryMappingProfile()));
            var mapper = new Mapper(cfg);
            fixture.Register<IMapper>(() => mapper);

            categoryRepositoryMock = fixture.Freeze<Mock<ICategoryRepository>>();
            categoryRepositoryMock
                .Setup(m => m.ListAllAsync())
                .ReturnsAsync(categories);
            categoryRepositoryMock
                .Setup(m => m.GetByIdAsync(ID))
                .ReturnsAsync(categories.First());
            categoryRepositoryMock
                .Setup(m => m.GetByIdAsync(INVALID_ID))
                .ReturnsAsync((Category)null);
            categoryRepositoryMock
                .Setup(m => m.AddAsync(It.IsAny<Category>()))
                .ReturnsAsync(categories.First());
        }

        public CategoriesController CreateSUT()
        {
            return fixture.Build<CategoriesController>().OmitAutoProperties().Create();
        }
    }
}
