using PlannerApp.Shared.Models;
using System;

namespace PlannerApp.Client.Services.ComponentHelperServices
{
    public interface ICategoryComponentService
    {
        void BeginAddingCategory(Action<CategoryDTO> onSave);
    }
}