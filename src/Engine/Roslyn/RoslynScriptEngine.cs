using Microsoft.CodeAnalysis.CSharp.Scripting;
using Repl.Core.Engine;
using System.Threading.Tasks;

namespace Repl.Engine.Roslyn
{
    public class RoslynScriptEngine : IScriptEngine
    {
        public async Task<IScriptResult> RunAsync(string code)
        {
            var result = await CSharpScript.RunAsync(code);
            return new ScriptResult(result);
        }
    }
}
