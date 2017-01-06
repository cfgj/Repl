using System.Threading.Tasks;
using Repl.Core.Engine;

namespace Repl.Core.Command
{
    public abstract class CommandBase : ICommand
    {
        public abstract string Name { get; }

        public abstract Task<CommandResult> ExecuteAsync(IScriptExecutor scriptExecutor, params string[] args);

        protected bool CorrectArgumentsCount(int count, params string[] args)
        {
            return args.Length == count;
        }
    }
}
