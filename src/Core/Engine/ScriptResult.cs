using System;

namespace Repl.Core.Engine
{
    public class ScriptResult : IScriptResult
    {
        public object ReturnedValue { get; set; }

        public Exception ExecutionException { get; set; }

        public Exception CompilationException { get; set; }

        public bool ExecutionFailed
        {
            get { return ExecutionException != null; }
        }

        public bool CompilationFailed
        {
            get { return CompilationException != null; }
        }
    }
}
