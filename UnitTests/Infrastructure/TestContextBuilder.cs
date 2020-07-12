using Blazorise;
using Blazorise.Bulma;
using Bunit;
using Bunit.Mocking.JSInterop;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PlannerApp.Client.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlannerApp.UnitTests.Infrastructure
{
    public class TestContextBuilder
    {
        public TestContextBuilder AddAuth(TestServiceProvider services)
        {
            var mock = new Mock<IAuthService>();
            services.AddScoped<IAuthService>(o => mock.Object);
            return this;
        }
    }

}
