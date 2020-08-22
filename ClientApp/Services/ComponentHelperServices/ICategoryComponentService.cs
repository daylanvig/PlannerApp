using Application.Categories.Queries.Common;
using System;

namespace ClientApp.Services.ComponentHelperServices
{
    public interface ICategoryComponentService
    {
        void BeginAddingCategory(Action<CategoryModel> onSave);
    }
}