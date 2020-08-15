using Application.PlannerItems.Commands.Shared;
using Application.PlannerItems.Queries.Common;
using PlannerApp.Shared.Models;
using System;

namespace PlannerApp.UnitTests.Infrastructure
{
    public class PlannerItemModelBuilder
    {
        private readonly PlannerItemModel item;
        public PlannerItemModelBuilder()
        {
            item = new PlannerItemModel
            {
                ID = 1,
                Description = "Test",
                PlannedActionDate = new DateTime(2020, 6, 6, 7, 0, 0),
                PlannedEndTime = new DateTime(2020, 6, 6, 8, 0, 0),
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length">length of event in minutes</param>
        /// <returns></returns>
        public PlannerItemModelBuilder WithTime(DateTime start, int length)
        {
            item.PlannedActionDate = start;
            item.PlannedEndTime = start.AddMinutes(length);
            return this;
        }

        public PlannerItemModelBuilder WithCategory(int categoryID)
        {
            item.CategoryID = categoryID;
            return this;
        }

        public PlannerItemModel Build()
        {
            return item;
        }
    }
}
