using Autofac;
using System.Linq;
using System.Reflection;

namespace Recorder
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<Logic>().As<ILogic>();
            builder.RegisterAssemblyTypes(Assembly.Load(nameof(Recorder)))
                .Where(t => t.Namespace.Contains("Actions"))
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name));
            return builder.Build();

        }
    }
}
