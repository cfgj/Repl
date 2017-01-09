using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repl.Core.Command;

namespace Repl.Core.Commands
{
    public interface IViewVarsCommand : ICommand { }

    public class ViewVarsCommand : IViewVarsCommand
    {
        public string Name
        {
            get { return "vars"; }
        }

        public string Description
        {
            get
            {
                return "Displays declared variables (their name and type).";
            }
        }

        public IDictionary<string, string> Parameters
        {
            get
            {
                return new Dictionary<string, string>
                {
                    { "short", "Displays only variable names." }
                };
            }
        }

        public string Usage
        {
            get
            {
                return "#vars [short]";
            }
        }

        public Task<CommandResult> ExecuteAsync(CommandContext context, params string[] args)
        {
            var shortMode = args.Count() == 1 && args[0] == "short";

            var vars = context.ScriptExecutor.Vars.OrderBy(v => v.Name);

            if (vars.Any())
            {
                var last = vars.Last();
                var stringVars = vars.Aggregate(
                    new StringBuilder(),
                    (acc, v) =>
                    {
                        if (shortMode)
                        {
                            acc.AppendFormat(v.Name == last.Name ? "{0}" : "{0}, ", v.Name);
                        }
                        else
                        {
                            acc.Append(' ', 4);
                            acc.AppendFormat(v.Name == last.Name ? "({0}, {1})" : "({0}, {1}),", v.Type.FullName, v.Name);
                            acc.AppendLine();
                        }
                        return acc;
                    },
                    acc => acc.ToString()
                );
                context.Console.WriteLine("[{0}{1}]", shortMode ? string.Empty : Environment.NewLine, stringVars);
            }
            else
            {
                context.Console.WriteLine("[]");
            }

            return Task.FromResult(new CommandResult(ExecutedCommandStatus.Success));
        }
    }
}
