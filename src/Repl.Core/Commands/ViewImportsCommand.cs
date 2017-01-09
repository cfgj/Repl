using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repl.Core.Command;

namespace Repl.Core.Commands
{
    public interface IViewImportsCommand : ICommand { }

    public class ViewImportsCommand : IViewImportsCommand
    {
        public string Name
        {
            get { return "imports"; }
        }

        public string Description
        {
            get
            {
                return "Displays imported namespaces.";
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
            get { return "#imports"; }
        }

        public Task<CommandResult> ExecuteAsync(CommandContext context, params string[] args)
        {
            var imports = context.ScriptExecutor.Imports.OrderBy(i => i);

            if (imports.Any())
            {
                var last = imports.Last();
                var stringImports = imports.Aggregate(
                    new StringBuilder(),
                    (acc, i) =>
                    {
                        acc.Append(' ', 4);
                        if (i == last)
                        {
                            acc.AppendLine(i);
                        }
                        else
                        {
                            acc.AppendFormat("{0},{1}", i, Environment.NewLine);
                        }
                        return acc;
                    },
                    acc => acc.ToString()
                );
                context.Console.WriteLine("[{0}{1}]", Environment.NewLine, stringImports);
            }
            else
            {
                context.Console.WriteLine("[]");
            }

            return Task.FromResult(new CommandResult(ExecutedCommandStatus.Success));
        }
    }
}
