using PlannerApp.Shared.Models;
using System;

namespace PlannerApp.Client.Services
{
    public interface IPlannerItemService
    {
        void ShowAddEditModal(PlannerItemDTO item, Action<PlannerItemDTO> onSave);
    }
}