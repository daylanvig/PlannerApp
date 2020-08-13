using PlannerApp.Shared.Models;
using System;

namespace PresentationClient.Services.ComponentHelperServices
{
    public interface ICategoryComponentService
    {
        void BeginAddingCategory(Action<CategoryDTO> onSave);
    }
}