using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repl.Core.Preprocessors;
using Repl.Utils;

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
            "System.Threading.Tasks",
            "System.Text"
        };

        protected readonly IScriptEngine _scriptEngine;

        protected readonly IScriptPreprocessor _preprocessor;

        protected HashSet<string> _imports;

        protected HashSet<string> _references;

        public ScriptExecutor(IScriptEngine scriptEngine, IScriptPreprocessor preprocessor)
        {
            ArgumentsGuard.ThrowIfNull(scriptEngine, nameof(scriptEngine));

            _scriptEngine = scriptEngine;
            _preprocessor = preprocessor;
            _references = new HashSet<string>();

            _imports = new HashSet<string>(DefaultImports);
        }

        public IEnumerable<string> Imports
        {
            get { return _imports; }
        }

        public IEnumerable<ScriptVariableData> Vars
        {
            get { return _scriptEngine.Vars; }
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
            var preprocessedScript = _preprocessor.Process(script);

            var allReferences = new HashSet<string>(DefaultReferences);
            allReferences.UnionWith(_references);
            allReferences.UnionWith(references);

            var importsIntersection = _imports.Intersect(preprocessedScript.Imports).ToList();

            _imports.UnionWith(preprocessedScript.Imports);
            _imports.UnionWith(imports);

            var result = await _scriptEngine.RunAsync(script, allReferences, _imports);

            if (!result.Success)
            {
                _imports.RemoveWhere(i => preprocessedScript.Imports.Except(importsIntersection).Contains(i));
            }

            return result;
        }

        public void Reset()
        {
            _references.Clear();
            _scriptEngine.Reset();
        }
    }
}
