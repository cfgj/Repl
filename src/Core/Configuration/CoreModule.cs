using Autofac;
using Repl.Core.Engine;

namespace Repl.Core.Configuration
{
    public sealed class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ScriptExecutor>().As<IScriptExecutor>();
        }
    }
}
