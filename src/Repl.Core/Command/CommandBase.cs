using System.Threading.Tasks;

namespace Repl.Core.Command
{
    public abstract class CommandBase : ICommand
    {
        public async Task<CommandResult> ExecuteAsync(CommandContext context, params string[] args)
        {
            if (!CorrectArgumentsCount(ArgumentsNumber, args))
            {
                return new CommandResult(ExecutedCommandStatus.Error, $"Wrong number of arguments ({args.Length} for {ArgumentsNumber}).");
            }
            return await InternalExecuteAsync(context, args);
        }

        #region Abstract members

        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract int ArgumentsNumber { get; }

        protected abstract Task<CommandResult> InternalExecuteAsync(CommandContext context, params string[] args);

        #endregion

        protected bool CorrectArgumentsCount(int count, params string[] args)
        {
            return args.Length == count;
        }
    }
}
