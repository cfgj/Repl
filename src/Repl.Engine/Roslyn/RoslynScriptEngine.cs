using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Repl.Core.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;

namespace Repl.Engine.Roslyn
{
    public class RoslynScriptEngine : IScriptEngine
    {
        protected ScriptOptions _scriptOptions { get; set; }

        protected ScriptState _sessionScriptState;

        public RoslynScriptEngine()
        {
            Reset();
        }

        #region IScriptEngine

        public IEnumerable<ScriptVariableData> Vars
        {
            get
            {
                if (_sessionScriptState != null)
                {
                    return GetVariables();
                }
                return new List<ScriptVariableData>();
            }
        }

        public void Reset()
        {
            _scriptOptions = ScriptOptions.Default;
            _sessionScriptState = null;
        }

        public virtual async Task<IScriptResult> RunAsync(string script, IEnumerable<string> references, IEnumerable<string> imports)
        {
            try
            {
                if (!IsComplete(script))
                {
                    return ScriptResult.Incomplete;
                }

                if (_sessionScriptState == null)
                {
                    _scriptOptions = _scriptOptions.WithReferences(references.ToList()).WithImports(imports.ToList());

                    var result = await CSharpScript.RunAsync(script, _scriptOptions);
                    _sessionScriptState = result;

                    return new ScriptResult(returnedValue: result.ReturnValue);
                }
                else
                {
                    _scriptOptions = _scriptOptions.WithReferences(references).WithImports(imports);

                    var result = await _sessionScriptState.ContinueWithAsync(script, _scriptOptions);
                    _sessionScriptState = result;

                    return new ScriptResult(returnedValue: result.ReturnValue);
                }
            }
            catch (CompilationErrorException ex)
            {
                return new ScriptResult(compilationException: ex);
            }
            catch (Exception ex)
            {
                return new ScriptResult(executionException: ex);
            }
        }

        #endregion

        #region Protected methods

        protected IEnumerable<ScriptVariableData> GetVariables()
        {
            return _sessionScriptState.Variables.GroupBy(v => v.Name).Select(gv => new ScriptVariableData
            {
                Name = gv.Key,
                Type = gv.Last().Type
            });
        }

        protected bool IsComplete(string script)
        {
            var syntaxTree = RoslynHelper.GetSyntaxTree(script);
            return SyntaxFactory.IsCompleteSubmission(syntaxTree);
        }

        #endregion
    }
}
