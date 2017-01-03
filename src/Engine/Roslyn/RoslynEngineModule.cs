using Autofac;
using Repl.Core.Configuration;
using Repl.Core.Engine;

namespace Repl.Engine.Roslyn
{
    public class RoslynEngineModule : EngineModule
    {
        public sealed override string Name
        { 
            get
            { 
                return "Roslyn";
            }
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RoslynScriptEngine>().As<IScriptEngine>();
            builder.RegisterType<ScriptResult>().As<IScriptResult>();
        }
    }
}
