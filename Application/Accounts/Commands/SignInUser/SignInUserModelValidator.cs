using FluentValidation;

namespace Application.Accounts.Commands.SignInUser
{
    public class SignInUserModelValidator : AbstractValidator<SignInUserModel>
    {
        public SignInUserModelValidator()
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
