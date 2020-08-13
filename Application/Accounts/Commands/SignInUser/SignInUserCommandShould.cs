using Application.Interfaces.Infrastructure;
using AutoMoq;
using AutoFixture;
using Domain.Accounts;
using Microsoft.Extensions.Configuration;
using Moq;
using Shared.TestSupport;
using Xunit;

namespace Application.Accounts.Commands.SignInUser
{
    public class SignInUserCommandShould
    {
        private readonly Fixture fixture;
        private readonly AutoMoqer mocker;
        private SignInUserCommand sut;
        
        private readonly IConfiguration config;
        private readonly PlannerAppUser user;

        private const string EMAIL = "email@email.ca";
        private const string TENANTID = "TENANT123";

        public SignInUserCommandShould()
        {
            fixture = TestFixture.Create();
            mocker = new AutoMoqer();
            user = new PlannerAppUser
            {
                Email = EMAIL,
                TenantID = TENANTID
            };
            mocker.SetInstance<IConfiguration>(new ConfigurationMock());
            mocker.GetMock<IUserService>()
                  .Setup(u => u.SignIn(It.IsAny<string>(), It.IsAny<string>()))
                  .ReturnsAsync(user);
            sut = mocker.Create<SignInUserCommand>();
        }

        [Fact]
        public void ReturnIsSuccessfulEqualsFalse()
        {
            // Arrange
            mocker.GetMock<IUserService>()
                  .Setup(u => u.SignIn(It.IsAny<string>(), It.IsAny<string>()))
                  .ReturnsAsync((PlannerAppUser)null);
            sut = mocker.Create<SignInUserCommand>();
            // Act
            var result = sut.Execute(mocker.Create<SignInUserModel>()).Result;
            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public void ReturnIsSuccessfulEqualsTrue()
        {
            Assert.True(sut.Execute(fixture.Create<SignInUserModel>()).Result.IsSuccessful);
        }

        [Fact]
        public void SetTenantIDAndNameClaims()
        {

        }
    }
}
