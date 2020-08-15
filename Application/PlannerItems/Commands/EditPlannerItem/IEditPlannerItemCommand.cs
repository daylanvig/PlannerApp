using Application.PlannerItems.Commands.Shared;
using Application.PlannerItems.Queries.Common;
using System.Threading.Tasks;

namespace Application.PlannerItems.Commands.EditPlannerItem
{
    public interface IEditPlannerItemCommand
    {
        Task<PlannerItemModel> Execute(int ID, PlannerItemCreateEditModel createEditModel);
    }
}
