using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repl.Core.Engine
{
    public interface IScriptExecutor
    {
        void AddReference(string reference);

        Task<IScriptResult> ExecuteAsync(string script);

        Task<IScriptResult> ExecuteAsync(string script, IEnumerable<string> references, IEnumerable<string> imports);

        void Reset();
    }
}
