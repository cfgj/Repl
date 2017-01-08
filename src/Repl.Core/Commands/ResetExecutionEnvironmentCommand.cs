using System.Threading.Tasks;
using Repl.Core.Command;

namespace Repl.Core.Commands
{
    public interface IResetExecutionEnvironmentCommand : ICommand { }

    public class ResetExecutionEnvironmentCommand : CommandBase, IResetExecutionEnvironmentCommand
    {
        public override string Name
        {
            get { return "reset"; }
        }

        public override string Description
        {
            get
            {
                return "Reset the execution environment to the initial state.";
            }
        }

        public override Task<CommandResult> ExecuteAsync(CommandContext context, params string[] args)
        {
            context.ScriptExecutor.Reset();
            var result = new CommandResult(ExecutedCommandStatus.Success, "Execution environment restored to the initial state.");

            return Task.FromResult(result);
        }
    }
}
