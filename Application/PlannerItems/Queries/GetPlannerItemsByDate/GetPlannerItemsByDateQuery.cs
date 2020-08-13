using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces.Persistence;
using Application.PlannerItems.Queries.Common;
using Domain.PlannerItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.PlannerItems.Queries.GetPlannerItemsByDate
{
    public class GetPlannerItemsByDateQuery : IGetPlannerItemsByDateQuery
    {
        private readonly IPlannerItemRepository repository;
        private readonly IMapper mapper;

        public GetPlannerItemsByDateQuery(IPlannerItemRepository database, IMapper mapper)
        {
            this.repository = database;
            this.mapper = mapper;
        }

        public async Task<IReadOnlyList<PlannerItemModel>> Execute(DateTime? startDate, DateTime? endDate)
        {
            IQueryable<PlannerItem> itemQuery;
            if (startDate.HasValue && endDate.HasValue)
            {
                itemQuery = repository.GetAll().Where(item => item.PlannedActionDate.Date >= startDate.Value.Date && item.PlannedEndTime.Date <= endDate.Value.Date);
            }
            else if (startDate.HasValue)
            {
                itemQuery = repository.GetAll().Where(item => item.PlannedActionDate.Date == startDate.Value.Date);
            }
            else
            {
                itemQuery = repository.GetAll();
            }
            return await mapper.ProjectTo<PlannerItemModel>(itemQuery).ToListAsync();
        }
    }
}
