using AutoMapper;
using PresentationServer.Categories;
using PresentationServer.PlannerItems;
using Xunit;

namespace PlannerApp.UnitTests
{
    public class MappingProfileTests
    {
        [Fact]
        public void PlannerItemMapValidation()
        {
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile<PlannerItemsMappingProfile>();
            });
            config.AssertConfigurationIsValid();
        }

        [Fact]
        public void CategoryMapValidation()
        {
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile<CategoryMappingProfile>();
            });
            config.AssertConfigurationIsValid();
        }
    }
}
