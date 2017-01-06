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

        public override int ArgumentsNumber
        {
            get { return 0; }
        }

        public override string Description
        {
            get
            {
                return "Reset the execution environment to the initial state.";
            }
        }

        protected override Task<CommandResult> InternalExecuteAsync(CommandContext context, params string[] args)
        {
            context.ScriptExecutor.Reset();
            var result = new CommandResult(ExecutedCommandStatus.Success, "Execution environment restored to the initial state.");

            return Task.FromResult(result);
        }
    }
}
