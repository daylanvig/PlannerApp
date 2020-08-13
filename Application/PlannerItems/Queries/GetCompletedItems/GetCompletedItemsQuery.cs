using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces.Persistence;
using Application.PlannerItems.Queries.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.PlannerItems.Queries.GetCompletedItems
{
    public class GetCompletedItemsQuery : IGetCompletedItemsQuery
    {
        private readonly IPlannerItemRepository plannerItemRepository;
        private readonly IMapper mapper;

        public GetCompletedItemsQuery(IPlannerItemRepository plannerItemRepository, IMapper mapper)
        {
            this.plannerItemRepository = plannerItemRepository;
            this.mapper = mapper;
        }

        public async Task<IReadOnlyList<PlannerItemModel>> Execute()
        {
            var query = plannerItemRepository.GetAll().Where(p => p.CompletionDate.HasValue);
            return await mapper.ProjectTo<PlannerItemModel>(query).ToListAsync();
        }
    }
}
