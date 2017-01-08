using System.Collections.Generic;
using System.Threading.Tasks;
using Repl.Core.Command;

namespace Repl.Core.Commands
{
    public interface IResetExecutionEnvironmentCommand : ICommand { }

    public class ResetExecutionEnvironmentCommand : IResetExecutionEnvironmentCommand
    {
        public string Name
        {
            get { return "reset"; }
        }

        public string Description
        {
            get
            {
                return "Reset the execution environment to the initial state.";
            }
        }

        public IDictionary<string, string> Parameters
        {
            get { return new Dictionary<string, string>(); }
        }

        public string Usage
        {
            get
            {
                return "#reset";
            }
        }

        public Task<CommandResult> ExecuteAsync(CommandContext context, params string[] args)
        {
            context.ScriptExecutor.Reset();
            var result = new CommandResult(ExecutedCommandStatus.Success, "Execution environment restored to the initial state.");

            return Task.FromResult(result);
        }
    }
}
