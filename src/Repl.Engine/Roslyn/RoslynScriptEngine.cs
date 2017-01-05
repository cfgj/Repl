using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Repl.Core.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;

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

        protected bool IsComplete(string code)
        {
            var options = new CSharpParseOptions(LanguageVersion.CSharp6, DocumentationMode.Parse, SourceCodeKind.Script, null);
            var syntaxTree = SyntaxFactory.ParseSyntaxTree(code, options);
            return SyntaxFactory.IsCompleteSubmission(syntaxTree);
        }
    }
}
