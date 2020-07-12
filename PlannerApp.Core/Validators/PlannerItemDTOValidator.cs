using FluentValidation;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PlannerApp.Shared.Validators
{
    public class PlannerItemDTOValidator : AbstractValidator<PlannerItemDTO>
    {
        public PlannerItemDTOValidator()
        {
            RuleFor(item => item.Description)
                .NotEmpty();

            RuleFor(item => item.PlannedActionDate)
                .NotNull();

            RuleFor(item => item.PlannedEndTime)
                .NotNull();

            RuleFor(item => item)
                .Custom((item, context) =>
                {
                    if (!(item.PlannedActionDate.HasValue && item.PlannedEndTime.HasValue && item.PlannedActionDate.Value < item.PlannedEndTime.Value))
                    {
                        context.AddFailure(nameof(item.PlannedEndTime), "End time must come after start time");
                    }
                });
        }
    }
}
