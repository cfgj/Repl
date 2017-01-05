using Autofac;
using Repl.Core.Console;
using Repl.Core.Engine;

namespace Repl.Core.Configuration
{
    public sealed class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ScriptExecutor>().As<IScriptExecutor>();
            builder.RegisterType<ReplConsole>().As<IReplConsole>();
            builder.RegisterType<Repl>().As<IRepl>();
        }
    }
}
