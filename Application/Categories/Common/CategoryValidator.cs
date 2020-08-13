using FluentValidation;
using PlannerApp.Shared.Models;

namespace Application.Categories.Common
{
    public class CategoryValidator : AbstractValidator<CategoryDTO>
    {
        public CategoryValidator()
        {
            RuleFor(m => m.Colour)
                .NotEmpty();

            RuleFor(m => m.Description)
                .NotEmpty();
        }
    }
}
