using System;
using System.Threading.Tasks;
using Autofac;
using Repl.Core.Engine;
using Repl.Engine.Roslyn;

namespace Repl
{
    public class Program
    {         
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new RoslynEngineModule());

            var container = builder.Build();

            var engine = container.Resolve<IScriptEngine>();

            Task.Run(async () => {
                var result = await engine.RunAsync("2+2");
                Console.WriteLine(result.ReturnedValue);
            }).Wait();
        }
    }
}
