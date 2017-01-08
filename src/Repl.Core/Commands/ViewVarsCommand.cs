using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repl.Core.Command;

namespace Repl.Core.Commands
{
    public interface IViewVarsCommand : ICommand { }

    public class ViewVarsCommand : CommandBase, IViewVarsCommand
    {
        public override string Name
        {
            get { return "vars"; }
        }

        public override string Description
        {
            get
            {
                return "Displays declared variables.";
            }
        }

        public override Task<CommandResult> ExecuteAsync(CommandContext context, params string[] args)
        {
            var shortMode = args.Count() == 1 && args[0] == "short";

            var vars = context.ScriptExecutor.Vars;

            if (vars.Any())
            {
                var last = vars.Last();
                var stringVars = vars.OrderBy(v => v.Name).Aggregate(
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
