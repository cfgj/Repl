using Autofac;
using Repl.Core;
using Repl.Core.Configuration;
using Repl.Core.Engine;
using Repl.Engine.Roslyn;
using System;
using System.Threading.Tasks;

namespace Repl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new CoreModule());
            builder.RegisterModule(new RoslynEngineModule());

            var container = builder.Build();

            var repl = container.Resolve<IRepl>();

            Task.Run(async () =>
            {
                await repl.OnAsync();

            }).Wait();
        }
    }
}
