using FluentValidation;

namespace Application.Accounts.Commands.RegisterNewUser
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(m => m.Email)
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("Invalid Email Address")
                .NotEmpty();
            RuleFor(m => m.Password)
                .MinimumLength(8);
        }
    }
}
