using AutoFixture;
using AutoMapper;
using Shared.TestSupport;

namespace Application.PlannerItems.Common
{
    public class PlannerItemsTestFixture : Fixture
    {
        public PlannerItemsTestFixture() : base()
        {
            TestFixture.AddDefaults(this);
            var mapperConfig = new MapperConfiguration(c => c.AddProfile(new PlannerItemsMappingProfile()));
            var mapper = new Mapper(mapperConfig);
            this.Register<IMapper>(() => mapper);
        }
    }
}
