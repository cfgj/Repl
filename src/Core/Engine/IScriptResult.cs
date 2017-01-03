using System;

namespace Repl.Core.Engine
{
    public interface IScriptResult
    {
        Exception Exception { get; }
        
        object ReturnedValue { get; }
    }
}
