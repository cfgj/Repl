using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repl.Core.Engine
{ 
    public interface IScriptExecutor
    {
        Task<IScriptResult> ExecuteAsync(string script);

        Task<IScriptResult> ExecuteAsync(string script, IEnumerable<string> references, IEnumerable<string> imports);
    }
}
