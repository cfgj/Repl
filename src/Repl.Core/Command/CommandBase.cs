using System.Threading.Tasks;

namespace Repl.Core.Command
{
    public abstract class CommandBase : ICommand
    {
        #region Abstract members

        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract Task<CommandResult> ExecuteAsync(CommandContext context, params string[] args);

        #endregion

        #region Protected methods

        protected bool CorrectArgumentsCount(int count, params string[] args)
        {
            return args.Length == count;
        }

        #endregion
    }
}
