using AutoMapper;
using PlannerApp.Server.Maps;
using System;
using System.Collections.Generic;
using System.Text;
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
                c.AddProfile<PlannerItemMappingProfile>();
            });
            config.AssertConfigurationIsValid();
        }
    }
}
