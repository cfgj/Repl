using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repl.Core.Command
{
    public interface ICommand
    {
        string Name { get; }

        string Description { get; }

        IDictionary<string, string> Parameters { get; }

        string Usage { get; }

        Task<CommandResult> ExecuteAsync(CommandContext context, params string[] args);
    }
}
