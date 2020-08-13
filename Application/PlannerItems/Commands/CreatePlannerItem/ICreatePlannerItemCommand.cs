using Application.PlannerItems.Commands.Shared;
using Application.PlannerItems.Queries.Common;
using System.Threading.Tasks;

namespace Application.PlannerItems.Commands.CreatePlannerItem
{
    public interface ICreatePlannerItemCommand
    {
        Task<PlannerItemModel> Execute(PlannerItemCreateEditModel model);
    }
}