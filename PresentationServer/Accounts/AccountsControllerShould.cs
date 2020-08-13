using Application.Accounts.Commands.RegisterNewUser;
using Application.Accounts.Commands.SignInUser;
using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PlannerApp.PresentationServer.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace PresentationServer.Accounts
{
    public class AccountsControllerShould
    {
        private readonly Fixture fixture;
        public AccountsControllerShould()
        {
            fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
        }

        [Fact]
        public void ReturnBadRequestRegisterResultFromRegisterEndpoint()
        {
            // Arrange
            var registerMock = Mock.Of<IRegisterNewUserCommand>(o =>
            o.Execute(It.IsAny<RegisterModel>()) == Task.FromResult(new RegisterResult() { IsSuccessful = false })
            );
            var sut = new AccountsController(registerMock, fixture.Create<ISignInUserCommand>());
            // Act
            var result = sut.Register(new RegisterModel()).Result;

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<RegisterResult>(badRequestResult.Value);
        }

        [Fact]
        public void ReturnOkRegisterResultFromRegisterEndpoint()
        {  // Arrange
            var registerMock = Mock.Of<IRegisterNewUserCommand>(o =>
            o.Execute(It.IsAny<RegisterModel>()) == Task.FromResult(new RegisterResult() { IsSuccessful = true }) 
            );
            var sut = new AccountsController(registerMock, Mock.Of<ISignInUserCommand>());
            // Act
            var result = sut.Register(new RegisterModel()).Result;

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<RegisterResult>(okObjectResult.Value);
        }

        [Fact]
        public void ReturnBadRequestFromLoginEndpoint()
        {
            // Arrange
            var signIn = Mock.Of<ISignInUserCommand>(o =>
            o.Execute(It.IsAny<SignInUserModel>()) == Task.FromResult(new SignInUserResult() { IsSuccessful = false })
            );
            var sut = new AccountsController(fixture.Create<IRegisterNewUserCommand>(), signIn);
            // Act
            var result = sut.LogIn(new SignInUserModel()).Result;

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SignInUserResult>(badRequestResult.Value);
        }

        [Fact]
        public void ReturnOkFromRegisterEndpoint()
        {
            // Arrange
            var signIn = Mock.Of<ISignInUserCommand>(o =>
            o.Execute(It.IsAny<SignInUserModel>()) == Task.FromResult(new SignInUserResult() { IsSuccessful = true })
            );
            var sut = new AccountsController(fixture.Create<IRegisterNewUserCommand>(), signIn);
            // Act
            var result = sut.LogIn(new SignInUserModel()).Result;
            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<SignInUserResult>(okObjectResult.Value);
        }
    }
}
