using Microsoft.CodeAnalysis.Scripting;
using Repl.Core.Engine;
using System;

namespace Repl.Engine.Roslyn
{
    public class ScriptResult : IScriptResult
    {
        public static readonly ScriptResult Empty = new ScriptResult();

        protected ScriptResult() { }

        public ScriptResult(ScriptState state)
        {
            Exception = state.Exception;
            ReturnedValue = state.ReturnValue;
        }

        public Exception Exception { get; private set; }

        public object ReturnedValue { get; private set; }
    }
}
