using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PlannerApp.Client.Components.AccountComponents;
using PlannerApp.Client.Services;
using PlannerApp.Shared.Models.Account;
using PlannerApp.UnitTests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PlannerApp.UnitTests.ComponentTests
{
    public class LoginFormShould
    {
 

        [Fact]
        public void RenderCorrectly()
        {
            // Arrange
            var ctx = new TestContext();
            var builder = new TestContextBuilder(ctx);
            builder.AddAuth();

            // Act
            var cut = ctx.RenderComponent<LoginForm>();

            // Assert
            cut.MarkupMatches(@"<div class='box'>
                <h1 class='title is-5'>Welcome back,</h1>
                <h2 class='subtitle is-5 has-text-grey-light'>sign in to continue.</h2>
                <form>
                    <div class='field '>
                        <label class='label'>Email Address</label>
                        <div class='control'>
                            <input name='Email' type = 'text' class='input  valid'>
                        </div>
                        <p class='help '></p>
                        </div>
                    <div class='field '>
                        <label class='label'>Password</label>
                        <div class='control'>
                            <input name='Password' type = 'password' class='input  valid'>  
                        </div>
                        <p class='help '></p>
                    </div>
                    <div class='field has-text-centered has-padding-top-20'>
                        <div class='control'>        
                            <button type = 'submit' class='button has-width-200 is-success'>
                                Sign In
                            </button>
                            <div class='has-padding-10'>or</div>
                            <button type = 'button' class='button has-width-200 is-outlined is-primary'>
                                Sign Up
                            </button>
                        </div>
                    </div>
                </form>
            </div>");
        }

        [Fact]
        public void NotSubmitLoginRequestIfFormIsNotFilledIn()
        {
            // Arrange
            var ctx = new TestContext();
            var spy = new Mock<IAuthService>();
            ctx.Services.AddScoped<IAuthService>(o => spy.Object);
            var cut = ctx.RenderComponent<LoginForm>();
            var form = cut.Find("form");

            // Act
            form.Submit();

            // Assert
            spy.VerifyNoOtherCalls();
        }

        [Fact]
        public void LoginWhenFormIsValidAndSaveIsClicked()
        {
            // Arrange
            var ctx = new TestContext();
            var spy = new Mock<IAuthService>();
            ctx.Services.AddScoped<IAuthService>(o => spy.Object);
            var cut = ctx.RenderComponent<LoginForm>(p =>
            {
                p.Add(p => p.LoginModel, new LoginModel
                {
                    Email = "testemail@testmail.test",
                    Password = "test123323"
                });
            });
            var form = cut.Find("form");

            // Act
            form.Submit();

            // Assert
            spy.Verify(o => o.Login(It.IsAny<LoginModel>()), Times.Once);
        }
    }
}
