using Autofac;
using Repl.Core.Commands;
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
            builder.RegisterType<ReturnedValueSerializer>().As<IReturnedValueSerializer>();
            builder.RegisterType<ReplConsole>().As<IReplConsole>();

            RegisterCommands(builder);
        }

        private void RegisterCommands(ContainerBuilder builder)
        {
            builder.RegisterType<AddReferenceCommand>().As<IAddReferenceCommand>();
            builder.RegisterType<LoadScriptCommand>().As<ILoadScriptCommand>();
            builder.RegisterType<ResetExecutionEnvironmentCommand>().As<IResetExecutionEnvironmentCommand>();
            builder.RegisterType<HelpCommand>().As<IHelpCommand>();
        }
    }
}
