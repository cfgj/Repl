using Autofac;
using Repl.Core.Console;
using Repl.Core.Engine;
using Repl.Core.Serialization;

namespace Repl.Core.Configuration
{
    public sealed class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ScriptExecutor>().As<IScriptExecutor>();
            builder.RegisterType<ReplConsole>().As<IReplConsole>();
            builder.RegisterType<ReturnedValueSerializer>().As<IReturnedValueSerializer>();
            builder.RegisterType<Repl>().As<IRepl>();
        }
    }
}
