using System;

namespace Repl.Core.Engine
{
    public class ScriptResult : IScriptResult
    {
        public static readonly ScriptResult Incomplete = new ScriptResult { IsComplete = false };

        public ScriptResult(object returnedValue = null,
                            Exception executionException = null,
                            Exception compilationException = null)
        {
            ReturnedValue = returnedValue;
            ExecutionException = executionException;
            CompilationException = compilationException;
            IsComplete = true;
        }

        public object ReturnedValue { get; private set; }

        public Exception ExecutionException { get; private set; }

        public Exception CompilationException { get; private set; }

        public bool ExecutionFailed
        {
            get { return ExecutionException != null; }
        }

        public bool CompilationFailed
        {
            get { return CompilationException != null; }
        }

        public bool IsComplete { get; private set; }
    }
}
