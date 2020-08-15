using Application.Interfaces.Persistence;
using Application.PlannerItems.Commands.Shared;
using Application.PlannerItems.Queries.Common;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace Application.PlannerItems.Commands.EditPlannerItem
{
    public class EditPlannerItemCommand : IEditPlannerItemCommand
    {
        private readonly IPlannerItemRepository plannerItemRepository;
        private readonly IMapper mapper;

        public EditPlannerItemCommand(IPlannerItemRepository plannerItemRepository, IMapper mapper)
        {
            this.plannerItemRepository = plannerItemRepository;
            this.mapper = mapper;
        }

        public async Task<PlannerItemModel> Execute(int id, PlannerItemCreateEditModel editModel)
        {
            var item = await plannerItemRepository.GetByIdAsync(id);
            if (item == null)
            {
                throw new ArgumentException("ID does not exist", nameof(id));
            }
            mapper.Map(editModel, item);
            await plannerItemRepository.UpdateAsync(item);
            return mapper.Map<PlannerItemModel>(item);
        }
    }
}
