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
            "System.Core"
        };

        public static readonly string[] DefaultImports =
        {
            "System",
            "System.Collections.Generic",
            "System.IO",
            "System.Text"
        };

        protected IScriptEngine _scriptEngine;

        protected HashSet<string> _references;

        public ScriptExecutor(IScriptEngine scriptEngine)
        {
            _scriptEngine = scriptEngine;

            _references = new HashSet<string>();
        }

        public void AddReference(string reference)
        {
            _references.Add(reference);
        }

        public async Task<IScriptResult> ExecuteAsync(string script)
        {
            return await ExecuteAsync(script, new string[0], new string[0]);
        }

        public async Task<IScriptResult> ExecuteAsync(string script, IEnumerable<string> references, IEnumerable<string> imports)
        {
            var allReferences = new HashSet<string>(DefaultReferences);
            allReferences.UnionWith(_references);
            allReferences.UnionWith(references);

            var allImports = DefaultImports.Union(imports).Distinct();

            return await _scriptEngine.RunAsync(script, allReferences, allImports);
        }

        public void Reset()
        {
            _references.Clear();
            _scriptEngine.Reset();
        }
    }
}
