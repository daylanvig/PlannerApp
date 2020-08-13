using Shared.TestSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMoq;
using AutoFixture;
using Xunit;
using FluentValidation.TestHelper;

namespace Application.Accounts.Commands.SignInUser
{
    public class SignInUserModelValidatorShould
    {
        private readonly SignInUserModelValidator sut;
        private readonly SignInUserModel model;
        public SignInUserModelValidatorShould()
        {
            var fixture = TestFixture.Create();
            model = fixture.Create<SignInUserModel>();
            model.Email = "test@test.com";
            sut = new SignInUserModelValidator();
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


        [Fact]
        public void NotHaveAnyErrors()
        {
            sut.TestValidate(model).ShouldNotHaveAnyValidationErrors();
        }
    }
}
