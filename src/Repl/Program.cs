using Autofac;
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

            var executor = container.Resolve<IScriptExecutor>();

            Task.Run(async () =>
            {
                IScriptResult result;
                result = await executor.ExecuteAsync("var a = \"a\";");
                WriteResult(result);
                result = await executor.ExecuteAsync("var b = \"b\";");
                WriteResult(result);
                result = await executor.ExecuteAsync("Path.Combine(new string[] { a, b })");
                WriteResult(result);
            }).Wait();
        }

        private static void WriteResult(IScriptResult result)
        {
            if (result.ExecutionFailed)
                Console.WriteLine("Execution failed.");
            else if (result.CompilationFailed)
                Console.WriteLine("Compilation failed.");
            else
                Console.WriteLine(result.ReturnedValue ?? "(nothing)");
        }
    }
}
