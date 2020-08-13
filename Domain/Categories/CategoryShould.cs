using Domain.PlannerItems;
using System;
using System.Collections.Generic;
using Xunit;

namespace Domain.Categories
{
    public class CategoryShould
    {
        private readonly Category sut;
        private const int ID = 1;
        private const string DESCRIPTION = "DESCRIPTION";
        private const string COLOUR = "RED";
        private readonly ICollection<PlannerItem> plannerItems = Array.Empty<PlannerItem>();
        public CategoryShould()
        {
            sut = new Category();
        }

        [Fact]
        public void SetAndGetID()
        {
            sut.ID = ID;
            Assert.Equal(ID, sut.ID);
        }

        [Fact]
        public void SetAndGetDescription()
        {
            sut.Description = DESCRIPTION;
            Assert.Equal(DESCRIPTION, sut.Description);
        }

        [Fact]
        public void SetAndGetColour()
        {
            sut.Colour = COLOUR;
            Assert.Equal(COLOUR, sut.Colour);
        }

        [Fact]
        public void SetAndGetPlannerItems()
        {
            sut.PlannerItems = plannerItems;
            Assert.Equal(plannerItems, sut.PlannerItems);
        }
    }
}
