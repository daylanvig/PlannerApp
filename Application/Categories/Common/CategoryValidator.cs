using Application.Categories.Queries.Common;
using FluentValidation;

namespace Application.Categories.Common
{
    public class CategoryValidator : AbstractValidator<CategoryModel>
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
