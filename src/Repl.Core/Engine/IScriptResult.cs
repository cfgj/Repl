using System;

namespace Repl.Core.Engine
{
    public interface IScriptResult
    {
        bool IsComplete { get; }

        object ReturnedValue { get; }

        Exception ExecutionException { get; }

        Exception CompilationException { get; }

        bool ExecutionFailed { get; }

        bool CompilationFailed { get; }
    }
}
