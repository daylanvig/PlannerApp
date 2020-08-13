using Domain.Categories;
using System;
using Xunit;

namespace Domain.PlannerItems
{
    public class PlannerItemShould
    {
        private readonly PlannerItem sut;
        private const int ID = 1;
        private const string DESCRIPTION = "TESTDESCRIPTION";
        private readonly DateTime DATE = new DateTime(2019, 2, 2, 2, 2, 2);
        public PlannerItemShould()
        {
            sut = new PlannerItem();
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
        public void SetAndGetPlannedActionDate()
        {
            sut.PlannedActionDate = DATE;
            Assert.Equal(DATE, sut.PlannedActionDate);
        }

        [Fact]
        public void SetAndGetPlannedEndtime()
        {
            sut.PlannedEndTime = DATE;
            Assert.Equal(DATE, sut.PlannedEndTime);
        }

        [Fact]
        public void SetAndGetCompletionDate()
        {
            sut.CompletionDate= DATE;
            Assert.Equal(DATE, sut.CompletionDate);
        }

        [Fact]
        public void SetAndGetCategoryID()
        {
            sut.CategoryID = ID;
            Assert.Equal(ID, sut.CategoryID);
        }

        [Fact]
        public void SetAndGetCategory()
        {
            var category = new Category();
            sut.Category = category;
            Assert.Equal(category, sut.Category);
        }
    }
}
