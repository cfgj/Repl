using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repl.Core.Command;

namespace Repl.Core.Commands
{
    public interface IHelpCommand : ICommand { }

    public class HelpCommand : IHelpCommand
    {
        public string Name
        {
            get { return "help"; }
        }

        public string Description
        {
            get
            {
                return "Displays commands with their description. If argument is specified the help for the specific command will be display.";
            }
        }

        public IDictionary<string, string> Parameters
        {
            get
            {
                return new Dictionary<string, string>
                {
                    { "command-name", "Command name." }
                };
            }
        }

        public string Usage
        {
            get
            {
                return "#help [command-name]";
            }
        }

        public Task<CommandResult> ExecuteAsync(CommandContext context, params string[] args)
        {
            var commandName = args.FirstOrDefault();

            if (string.IsNullOrEmpty(commandName))
            {
                context.Console.WriteLine($"Commands available int the REPL:");
                foreach (var command in context.Commands.OrderBy(c => c.Key))
                {
                    context.Console.WriteLine("{0} : {1}", command.Key, command.Value.Description);
                }
            }
            else if (context.Commands.ContainsKey(commandName))
            {
                var command = context.Commands[commandName];

                context.Console.WriteLine(command.Description);
                context.Console.WriteLine();

                context.Console.WriteLine("Usage:");
                context.Console.WriteLine(command.Usage);
                context.Console.WriteLine();

                if (command.Parameters.Any())
                {
                    context.Console.WriteLine("Parameters:");
                    foreach (var parameter in command.Parameters)
                    {
                        context.Console.WriteLine("{0}\t{1}", parameter.Key, parameter.Value);
                    }
                    context.Console.WriteLine();
                }
            }
            else
            {
                context.Console.WriteLine($"Command \"{commandName}\" does not exist.", ConsoleColor.Red);
            }

            return Task.FromResult(new CommandResult(ExecutedCommandStatus.Success));
        }
    }
}
