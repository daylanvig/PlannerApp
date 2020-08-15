using Application.Interfaces.Persistence;
using AutoFixture;
using AutoMapper;
using Domain.PlannerItems;
using Moq;
using Shared.TestSupport;
using System.Collections.Generic;
using System.Linq;

namespace Application.PlannerItems.Common
{
    public class PlannerItemsTestFixture : Fixture
    {
        public List<PlannerItem> Items = new List<PlannerItem>();

        public readonly Mock<IPlannerItemRepository> repositoryMock;
        public PlannerItemsTestFixture() : base()
        {
            TestFixture.AddDefaults(this);
            var mapperConfig = new MapperConfiguration(c => c.AddProfile(new PlannerItemsMappingProfile()));
            var mapper = new Mapper(mapperConfig);
            this.Register<IMapper>(() => mapper);
            this.AddManyTo(Items, 10);
            repositoryMock = this.Freeze<Mock<IPlannerItemRepository>>();

             repositoryMock
                .Setup(p => p.GetAll())
                .Returns(() => Items.AsAsyncQueryable());

            repositoryMock
                .Setup(m => m.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Items.First());
        }
    }
}
