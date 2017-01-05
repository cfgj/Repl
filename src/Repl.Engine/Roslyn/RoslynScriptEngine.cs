using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Repl.Core.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repl.Engine.Roslyn
{
    public class RoslynScriptEngine : IScriptEngine
    {
        protected ScriptOptions _scriptOptions { get; set; }

        protected ScriptState _sessionScriptState;

        public RoslynScriptEngine()
        {
            _scriptOptions = ScriptOptions.Default;
        }

        public virtual async Task<IScriptResult> RunAsync(string script, IEnumerable<string> references, IEnumerable<string> imports)
        {
            try
            {
                if (_sessionScriptState == null)
                {
                    _scriptOptions = _scriptOptions.WithReferences(references.ToList()).WithImports(imports.ToList());

                    var result = await CSharpScript.RunAsync(script, _scriptOptions);

                    _sessionScriptState = result;

                    return new ScriptResult { ReturnedValue = result.ReturnValue };
                }
                else
                {
                    _scriptOptions = _scriptOptions.WithReferences(references).WithImports(imports);

                    var result = await _sessionScriptState.ContinueWithAsync(script, _scriptOptions);

                    _sessionScriptState = result;

                    return new ScriptResult { ReturnedValue = result.ReturnValue };
                }
            }
            catch (CompilationErrorException ex)
            {
                return new ScriptResult { CompilationException = ex };
            }
            catch (Exception ex)
            {
                return new ScriptResult { ExecutionException = ex };
            }
        }
    }
}
