using System.Threading.Tasks;
using Repl.Core.Engine;

namespace Repl.Core.Command
{
    public interface ICommand
    {
        string Name { get; }

        Task<CommandResult> ExecuteAsync(IScriptExecutor scriptExecutor, params string[] args);
    }
}
