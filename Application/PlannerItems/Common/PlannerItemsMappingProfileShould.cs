using AutoMapper;
using Xunit;

namespace Application.PlannerItems.Common
{
    public class PlannerItemsMappingProfileShould
    {
        [Fact]
        public void MapCorrectly()
        {
            var cfg = new MapperConfiguration(c => c.AddProfile(new PlannerItemsMappingProfile()));
            var mapper = new Mapper(cfg);
            cfg.AssertConfigurationIsValid();
        }
    }
}
