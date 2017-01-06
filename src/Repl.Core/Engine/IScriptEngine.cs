using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repl.Core.Engine
{
    public interface IScriptEngine
    {
        void Reset();

        Task<IScriptResult> RunAsync(string script, IEnumerable<string> references, IEnumerable<string> imports);
    }
}
