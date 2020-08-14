using Application.Interfaces.Persistence;
using AutoFixture;
using AutoMapper;
using Domain.PlannerItems;
using Moq;
using Shared.TestSupport;
using System.Collections.Generic;

namespace Application.PlannerItems.Common
{
    public class PlannerItemsTestFixture : Fixture
    {
        public List<PlannerItem> Items = new List<PlannerItem>();
        public PlannerItemsTestFixture() : base()
        {
            TestFixture.AddDefaults(this);
            var mapperConfig = new MapperConfiguration(c => c.AddProfile(new PlannerItemsMappingProfile()));
            var mapper = new Mapper(mapperConfig);
            this.Register<IMapper>(() => mapper);
            this.AddManyTo(Items, 10);
            this.Freeze<Mock<IPlannerItemRepository>>()
             .Setup(p => p.GetAll())
             .Returns(() => Items.AsAsyncQueryable());
        }
    }
}
