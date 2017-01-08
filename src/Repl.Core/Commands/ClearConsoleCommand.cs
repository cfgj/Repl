using System.Collections.Generic;
using System.Threading.Tasks;
using Repl.Core.Command;

namespace Repl.Core.Commands
{
    public interface IClearConsoleCommand : ICommand { }

    public class ClearConsoleCommand : IClearConsoleCommand
    {
        public string Name
        {
            get { return "clear"; }
        }

        public string Description
        {
            get
            {
                return "Clears console.";
            }
        }

        public IDictionary<string, string> Parameters
        {
            get
            {
                return new Dictionary<string, string>();
            }
        }

        public string Usage
        {
            get
            {
                return "#clear";
            }
        }

        public Task<CommandResult> ExecuteAsync(CommandContext context, params string[] args)
        {
            context.Console.Clear();

            return Task.FromResult(new CommandResult(ExecutedCommandStatus.Success));
        }
    }
}
