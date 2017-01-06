using System.Threading.Tasks;

namespace Repl.Core.Command
{
    public interface ICommand
    {
        string Name { get; }

        string Description { get; }

        Task<CommandResult> ExecuteAsync(CommandContext context, params string[] args);
    }
}
