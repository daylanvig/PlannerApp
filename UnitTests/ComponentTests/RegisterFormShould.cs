using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PlannerApp.Client.Components.AccountComponents;
using PlannerApp.Client.Services;
using PlannerApp.Shared.Models.Account;
using PlannerApp.UnitTests.Infrastructure;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PlannerApp.UnitTests.ComponentTests
{
    public class RegisterFormShould : IClassFixture<TestContextBuilder>
    {
        TestContextBuilder fixture;
        public RegisterFormShould(TestContextBuilder fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void RenderCorrectly()
        {
            // Arrange
            using var ctx = new TestContext();
            fixture.AddAuth(ctx.Services);

            // Act
            var cut = ctx.RenderComponent<RegisterForm>();

            // Assert
            cut.MarkupMatches(@"<div class=""box"">
                      <h1 class=""title is-5"">Welcome,</h1>
                      <h2 class=""subtitle is-5 has-text-grey-light"">the following is all that's required.</h2>
                      <form >
                        <div class=""field "">
                          <label class=""label"">Email Address</label>
                          <div class=""control"">
                            <input name=""Email"" type=""text"" class=""input valid"" >
                          </div>
                          <p class=""help "">
                          </p>
                        </div>
                        <div class=""field "">
                          <label class=""label"">Password</label>
                          <div class=""control"">
                            <input autocomplete=""new-password"" name=""Password"" type=""password"" class=""input valid"" >
                          </div>
                          <p class=""help "">
                          </p>
                        </div>
                        <div class=""field "">
                          <label class=""label"">Confirm Password</label>
                          <div class=""control"">
                            <input autocomplete=""new-password"" name=""ConfirmPassword"" type=""password"" class=""input valid"" >
                          </div>
                          <p class=""help "">
                          </p>
                        </div>
                        <div class=""field has-text-centered has-padding-top-20"">
                          <div class=""control"">
                            <button type=""submit"" class=""button has-width-200 is-success"">
                              Sign Up
                            </button>
                            <div class=""has-padding-10"">or</div>
                            <button  type=""button"" class=""button has-width-200 is-outlined is-primary"">
                              Sign In
                            </button>
                          </div>
                        </div>
                      </form>
                    </div>");

        }

        [Fact]
        public void SubmitRegistrationDetailsToTheAuthServiceWhenSaved()
        {
            // Arrange
            using var ctx = new TestContext();
            var spy = new Mock<IAuthService>();
            spy.Setup(o => o.Register(It.IsAny<RegisterModel>())).Returns(Task.FromResult(new RegisterResult()));
            ctx.Services.AddScoped<IAuthService>(o => spy.Object);
            var accountDetails = new Shared.Models.Account.RegisterModel
            {
                Email = "testemail@gmail.com",
                Password = "test123213123",
                ConfirmPassword = "test123213123"
            };
            var cut = ctx.RenderComponent<RegisterForm>(parameters =>
            {
                parameters.Add(p => p.GoToLoginAction, Mock.Of<Action>());
            });
            cut.Instance.RegisterModel = accountDetails;
            cut.Render();
            var registerForm = cut.Find("form");

            // Act
            registerForm.Submit();

            // Assert
            spy.Verify(o => o.Register(It.IsAny<RegisterModel>()), Times.Once);
        }
    }
}
