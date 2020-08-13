using Application.Interfaces.Infrastructure;
using AutoFixture;
using AutoMoq;
using Domain.Accounts;
using Microsoft.AspNetCore.Identity;
using Moq;
using Shared.TestSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Accounts.Commands.RegisterNewUser
{
    public class RegisterNewUserCommandShould
    {

        private RegisterNewUserCommand sut;
        private PlannerAppUser user;

        private readonly Mock<IUserService> userServiceMock;
        private readonly RegisterModel registerModel;

        private const string EMAIL = "email@email.ca";
        private const string PASSWORD = "Password123";

        public RegisterNewUserCommandShould()
        {
            registerModel = new RegisterModel
            {
                Email = EMAIL,
                Password = PASSWORD,
                ConfirmPassword = PASSWORD
            };
            userServiceMock = new Mock<IUserService>();
            userServiceMock
                .Setup(u => u.CreateUser(It.IsAny<PlannerAppUser>(), It.IsAny<string>()))
                .Callback<PlannerAppUser, string>((PlannerAppUser user, string password) =>
                {
                    this.user = user;
                })
                .Returns(Task.FromResult(IdentityResult.Success));
            sut = new RegisterNewUserCommand(userServiceMock.Object);
        }

        [Fact]
        public void SetTenantIDToGUID()
        {
            sut.Execute(registerModel).GetAwaiter().GetResult();
            Assert.True(Guid.TryParse(user.TenantID, out var _));
        }

        [Fact]
        public void UserRegisterModelEmail()
        {
            sut.Execute(registerModel).GetAwaiter().GetResult();
            Assert.Equal(EMAIL, user.Email);
        }

        [Fact]
        public void ReturnIsSuccessful()
        {
            var result = sut.Execute(registerModel).Result;
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void ReturnIsSuccessfulEqualsFalseWithErrors()
        {
            // Arrange
            var failResult = IdentityResult.Failed(new IdentityError() { Code = "code", Description = "desc" });
            userServiceMock
                .Setup(u => u.CreateUser(It.IsAny<PlannerAppUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(failResult));
            sut = new RegisterNewUserCommand(userServiceMock.Object);
            // Act
            var result = sut.Execute(registerModel).Result;
            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Single(result.Errors);
            Assert.Equal("desc", result.Errors.First());
        }
    }
}
