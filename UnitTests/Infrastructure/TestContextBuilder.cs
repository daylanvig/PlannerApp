using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using PlannerApp.Client.Services;
using PlannerApp.Shared.Services;
using System;
using System.Threading.Tasks;

namespace PlannerApp.UnitTests.Infrastructure
{
    public class TestContextBuilder
    {
        private readonly TestContext ctx;
        public TestContextBuilder(TestContext ctx = null)
        {
            this.ctx = ctx ?? new TestContext();
        }

        public TestContext Build()
        {
            return ctx;
        }

        public TestContextBuilder AddCommon()
        {
            AddAppState();
            AddAuth();
            AddDateProvider();
            AddDOMInteropServiceProvider();
            return this;
        }

        public TestContextBuilder AddDOMInteropServiceProvider()
        {
            var domService = new Mock<IDOMInteropService>();
            domService.Setup(o => o.ScrollIntoView(It.IsAny<string>())).Returns(Task.CompletedTask);
            ctx.Services.AddScoped<IDOMInteropService>(o => domService.Object);
            return this;
        }

        public TestContextBuilder AddDateProvider()
        {
            var dateProvider = Mock.Of<IDateTimeProvider>(o => o.Now == new DateTime(2020, 6, 6, 15, 0, 0));
            ctx.Services.AddTransient<IDateTimeProvider>(o => dateProvider);
            return this;
        }

        public TestContextBuilder AddAppState()
        {
            var state = new Mock<IAppState>();
            ctx.Services.AddSingleton<IAppState>(o => state.Object);
            return this;
        }

        public TestContextBuilder AddAuth()
        {
            var mock = new Mock<IAuthService>();
            ctx.Services.AddScoped<IAuthService>(o => mock.Object);
            return this;
        }

        public TestContextBuilder ReplaceService<T>(T service) where T : class
        {
            ctx.Services.RemoveAll(service.GetType());
            ctx.Services.AddScoped(service.GetType(), o => service);
            return this;
        }
    }

}
