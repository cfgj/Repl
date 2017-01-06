using Autofac;
using Repl.Core.Configuration;
using Repl.Engine.Roslyn;
using System.Threading.Tasks;

namespace Repl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Repl>().As<Repl>();
            builder.RegisterModule(new CoreModule());
            builder.RegisterModule(new RoslynEngineModule());

            var container = builder.Build();

            var repl = container.Resolve<Repl>();

            Task.Run(async () =>
            {
                await repl.OnAsync();

            }).Wait();
        }
    }
}
