using Application.Categories.Queries.Common;
using System;

namespace PresentationClient.Services.ComponentHelperServices
{
    public interface ICategoryComponentService
    {
        void BeginAddingCategory(Action<CategoryModel> onSave);
    }
}