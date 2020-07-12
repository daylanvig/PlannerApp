using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PlannerApp.Client.Services;

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
