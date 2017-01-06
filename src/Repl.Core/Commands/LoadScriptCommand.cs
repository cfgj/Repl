using System.IO;
using System.Threading.Tasks;
using Repl.Core.Command;
using Repl.Core.Engine;

namespace Repl.Core.Commands
{
    public interface ILoadScriptCommand : ICommand { }

    public class LoadScriptCommand : CommandBase, ILoadScriptCommand
    {
        public override string Name
        {
            get { return "load"; }
        }

        public override int ArgumentsNumber
        {
            get { return 1; }
        }

        public override string Description
        {
            get
            {
                return "Is used to execute a script file. Arguments: path.";
            }
        }

        protected override async Task<CommandResult> InternalExecuteAsync(CommandContext context, params string[] args)
        {
            var scriptPath = args[0];

            if (File.Exists(scriptPath))
            {
                var script = File.ReadAllText(scriptPath);
                var scriptResult = await context.ScriptExecutor.ExecuteAsync(script);

                var commandStatus = scriptResult.Success ? ExecutedCommandStatus.Success : ExecutedCommandStatus.Error;

                return new CommandResult(commandStatus, PrepareMessage(scriptResult));
            }
            else
            {
                return new CommandResult(ExecutedCommandStatus.Error, $"Script \"{scriptPath}\" not found.");
            }
        }

        private string PrepareMessage(IScriptResult result)
        {
            if (result.CompilationFailed)
            {
                return result.CompilationException.Message;
            }
            else if (result.ExecutionFailed)
            {
                return result.ExecutionException.Message;
            }
            return "Script executed properly.";
        }
    }
}
