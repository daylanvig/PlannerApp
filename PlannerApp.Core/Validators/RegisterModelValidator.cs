using FluentValidation;
using PlannerApp.Shared.Models.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlannerApp.Shared.Validators
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
