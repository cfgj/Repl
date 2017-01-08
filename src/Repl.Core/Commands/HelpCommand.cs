using System.Threading.Tasks;
using Repl.Core.Command;

namespace Repl.Core.Commands
{
    public interface IHelpCommand : ICommand { }

    public class HelpCommand : CommandBase, IHelpCommand
    {
        public override string Name
        {
            get { return "help"; }
        }

        public override string Description
        {
            get
            {
                return "Output all commands with description.";
            }
        }

        public override Task<CommandResult> ExecuteAsync(CommandContext context, params string[] args)
        {
            context.Console.WriteLine($"Commands available int the REPL:");
            foreach (var command in context.Commands)
            {
                context.Console.WriteLine($"#{command.Key} : {command.Value.Description}");
            }

            return Task.FromResult(new CommandResult(ExecutedCommandStatus.Success));
        }
    }
}
