using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Bunit;
using PlannerApp.Client.Components.AccountComponents;
using Microsoft.Extensions.DependencyInjection;
using PlannerApp.Client.Services;
using Moq;
using Blazorise;
using Blazorise.Bulma;
using Bunit.Mocking.JSInterop;

namespace PlannerApp.UnitTests.ComponentTests
{
    public class RegisterFormShould
    {

        [Fact]
        public void RenderCorrectly()
        {
            // Arrange
            using var ctx = new TestContext();
            var authMock = Mock.Of<IAuthService>();
            ctx.Services.AddSingleton<IAuthService>(authMock);
            ctx.Services.AddBlazorise().AddBulmaProviders();
            ctx.Services.AddMockJsRuntime();
            // Act
            var html = ctx.RenderComponent<RegisterForm>();

            // Assert
            html.MarkupMatches(@"<div class='box'>
                                    <h1 class='title is-5'>Welcome,</h1>
                                    <h2 class='subtitle is-5 has-text-grey-light'>the following is all that's required.</h2>
                                    <form>
                                        <div class='field' style=''>
                                            <label class='field-label' style=''>Email Address</label>
                                                <input id:ignore type='email' class='input' style=''>
                                        </div>
                                        <div class='field' style=''>
                                            <label class='field-label' style=''>Password</label>
                                            <input id:ignore type='password' class='input' style=''>   
                                        </div>
                                        <div class='field' style=''>
                                            <label class='field-label' style=''>Confirm Password</label>
                                            <input id:ignore type='password' class='input' style=''>
                                        </div>
                                        <div class='field has-text-centered has-padding-top-20' style=''>
                                            <button id:ignore type='submit' class='button is-success has-width-200' style=''>
                                                Sign Up
                                            </button>
                                            <div class='has-padding-10'>or</div>
                                            <button id:ignore type='button' class='button is-primary is-outlined has-width-200' style=''>
                                                Sign In
                                            </button>
                                        </div>
                                    </form>
                                </div>");
        }
    }
}
