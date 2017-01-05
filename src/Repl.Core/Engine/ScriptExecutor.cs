using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repl.Core.Engine
{
    public class ScriptExecutor : IScriptExecutor
    {
        public static readonly string[] DefaultReferences =
        {
            "System",
        };

        public static readonly string[] DefaultImports =
        {
            "System",
            "System.IO"
        };

        protected IScriptEngine _scriptEngine;

        public ScriptExecutor(IScriptEngine scriptEngine)
        {
            _scriptEngine = scriptEngine;
        }

        public async Task<IScriptResult> ExecuteAsync(string script)
        {
            return await ExecuteAsync(script, new string[0], new string[0]);
        }

        public async Task<IScriptResult> ExecuteAsync(string script, IEnumerable<string> references, IEnumerable<string> imports)
        {
            var allReferences = DefaultReferences.Union(references).Distinct();
            var allImports = DefaultImports.Union(imports).Distinct();

            return await _scriptEngine.RunAsync(script, allReferences, allImports);
        }
    }
}
