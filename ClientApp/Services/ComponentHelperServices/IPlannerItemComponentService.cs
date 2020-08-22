using Application.PlannerItems.Commands.Shared;
using Application.PlannerItems.Queries.Common;
using System;

namespace ClientApp.Services.ComponentHelperServices
{
    public interface IPlannerItemComponentService
    {
        void ShowAddEditModal(PlannerItemCreateEditModel item, Action<PlannerItemModel> onSave);
    }
}