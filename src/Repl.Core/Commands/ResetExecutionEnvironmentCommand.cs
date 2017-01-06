using System.Threading.Tasks;
using Repl.Core.Command;
using Repl.Core.Engine;

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

        protected override Task<CommandResult> InternalExecuteAsync(IScriptExecutor scriptExecutor, params string[] args)
        {
            scriptExecutor.Reset();
            var result = new CommandResult(ExecutedCommandStatus.Success, "Execution environment restored to the initial state.");

            return Task.FromResult(result);
        }
    }
}
