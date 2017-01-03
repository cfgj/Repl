using Autofac;

namespace Repl.Core.Configuration
{
    public abstract class EngineModule : Module
    {
         public abstract string Name { get; }
    }
}
