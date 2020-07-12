using FluentValidation;
using PlannerApp.Shared.Models.Account;

namespace PlannerApp.Shared.Validators
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(m => m.Email)
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("'{PropertyValue}' is not a valid email address.")
                .NotEmpty();
            RuleFor(m => m.Password)
                .MinimumLength(8);
        }
    }
}
