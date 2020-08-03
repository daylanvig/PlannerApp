using PlannerApp.Shared.Models;
using System;

namespace PlannerApp.Client.Services.ComponentHelperServices
{
    public interface IPlannerItemComponentService
    {
        void ShowAddEditModal(PlannerItemDTO item, Action<PlannerItemDTO> onSave);
    }
}