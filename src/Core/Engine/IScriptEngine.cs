using System.Threading.Tasks;

namespace Repl.Core.Engine
{
    public interface IScriptEngine
    {
        Task<IScriptResult> RunAsync(string code);            
    }
}
