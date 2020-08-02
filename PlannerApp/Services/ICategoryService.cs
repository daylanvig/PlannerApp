using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlannerApp.Client.Services
{
    public interface ICategoryService
    {
        void BeginAddingCategory(Action<CategoryDTO> onSave);
        Task<IDictionary<CategoryDTO, int>> GetTotalMinutesByCategory();
    }
}