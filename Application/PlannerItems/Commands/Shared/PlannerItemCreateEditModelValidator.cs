using FluentValidation;

namespace Application.PlannerItems.Commands.Shared
{
    public class PlannerItemCreateEditModelValidator : AbstractValidator<PlannerItemCreateEditModel>
    {
        public PlannerItemCreateEditModelValidator()
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
