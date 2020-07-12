using FluentValidation;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlannerApp.Shared.Validators
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
