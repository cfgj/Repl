using System.Threading.Tasks;
using Repl.Core.Engine;

namespace Repl.Core.Command
{
    public abstract class CommandBase : ICommand
    {
        public async Task<CommandResult> ExecuteAsync(IScriptExecutor scriptExecutor, params string[] args)
        {
            if (!CorrectArgumentsCount(ArgumentsNumber, args))
            {
                return new CommandResult(ExecutedCommandStatus.Error, $"Wrong number of arguments ({args.Length} for {ArgumentsNumber}).");
            }
            return await InternalExecuteAsync(scriptExecutor, args);
        }

        #region Abstract members

        public abstract string Name { get; }

        public abstract int ArgumentsNumber { get; }

        protected abstract Task<CommandResult> InternalExecuteAsync(IScriptExecutor scriptExecutor, params string[] args);

        #endregion

        protected bool CorrectArgumentsCount(int count, params string[] args)
        {
            return args.Length == count;
        }
    }
}
