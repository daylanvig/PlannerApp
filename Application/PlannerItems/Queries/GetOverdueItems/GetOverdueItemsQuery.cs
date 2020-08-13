using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces.Persistence;
using Application.PlannerItems.Queries.Common;
using PlannerApp.Shared.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.PlannerItems.Queries.GetOverdueItems
{
    public class GetOverdueItemsQuery : IGetOverdueItemsQuery
    {
        private readonly IPlannerItemRepository repository;
        private readonly IMapper mapper;
        private readonly IDateTimeProvider dateTimeProvider;

        public GetOverdueItemsQuery(IPlannerItemRepository plannerItemRepository, IMapper mapper, IDateTimeProvider dateTimeProvider)
        {
            this.repository = plannerItemRepository;
            this.mapper = mapper;
            this.dateTimeProvider = dateTimeProvider;
        }

        public async Task<IReadOnlyList<PlannerItemModel>> Execute()
        {
            var query = repository.GetAll()
                        .Where(item => item.CompletionDate == null && item.PlannedEndTime < dateTimeProvider.Now);
            return await mapper.ProjectTo<PlannerItemModel>(query).ToListAsync();
        }
    }
}
