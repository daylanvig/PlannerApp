using AutoFixture;
using FluentValidation.TestHelper;
using Shared.TestSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Accounts.Commands.RegisterNewUser
{
    public class RegisterModelValidatorShould
    {
        private readonly RegisterModelValidator sut;
        private readonly Fixture fixture;
        private readonly RegisterModel model;

        public RegisterModelValidatorShould()
        {
            fixture = TestFixture.Create();
            sut = fixture.Create<RegisterModelValidator>();
            model = fixture.Create<RegisterModel>();
            model.Email = "email@email.ca";
        }

        [Fact]
        public void NotHaveAnyValidationErrors()
        {
            var result = sut.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData("")]
        [InlineData("1234563")]
        public void HaveErrorForPassword(string password)
        {
            // Arrange
            model.Password = password;
            // Act
            var result = sut.TestValidate(model);
            // Assert
            result.ShouldHaveValidationErrorFor(v => v.Password);
        }

        [Theory]
        [InlineData("notEmail")]
        [InlineData("")]
        public void HaveErrorForEmail(string email)
        {
            // Arrange
            model.Email = email;
            // Act
            var result = sut.TestValidate(model);
            // Assert
            result.ShouldHaveValidationErrorFor(v => v.Email);
        }
    }
}
