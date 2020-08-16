using Autofac;
using Persistence;
using Persistence.Common;
using PlannerApp.Server;
using System.Linq;
using System.Reflection;

namespace PresentationServer.Dependencies
{
    public static class IOC
    {
        public static void BuildIOCContainer(this ContainerBuilder builder)
        {
            var dataAssembly = Assembly.GetAssembly(typeof(PlannerContext));
            builder.RegisterType<PlannerContext>().As<IPlannerContext>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(dataAssembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            var applicationAssembly = Assembly.GetAssembly(typeof(Application.Config));

            builder.RegisterAssemblyTypes(applicationAssembly)
                .Where(t => t.Name.EndsWith("Query") || t.Name.EndsWith("Command"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            var infrastructureAssembly = Assembly.GetAssembly(typeof(Infrastructure.Config));
            builder.RegisterAssemblyTypes(infrastructureAssembly)
                 .Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Wrapper"))
                 .AsImplementedInterfaces()
                 .InstancePerLifetimeScope();
        }
    }
}
