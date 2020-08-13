using AutoMapper;
using Application.Interfaces.Persistence;
using Application.PlannerItems.Commands.Shared;
using Domain.PlannerItems;
using System.Threading.Tasks;
using Application.PlannerItems.Queries.Common;

namespace Application.PlannerItems.Commands.CreatePlannerItem
{
    public class CreatePlannerItemCommand : ICreatePlannerItemCommand
    {
        private readonly IPlannerItemRepository plannerItemRepository;
        private readonly IMapper mapper;

        public CreatePlannerItemCommand(IPlannerItemRepository plannerItemRepository, IMapper mapper)
        {
            this.plannerItemRepository = plannerItemRepository;
            this.mapper = mapper;
        }

        public async Task<PlannerItemModel> Execute(PlannerItemCreateEditModel model)
        {
            var plannerItem = mapper.Map<PlannerItem>(model);
            await plannerItemRepository.AddAsync(plannerItem);
            return mapper.Map<PlannerItemModel>(plannerItem);
        }
    }
}
