using System;
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
            get { return "l"; }
        }

        public override async Task<CommandResult> ExecuteAsync(IScriptExecutor scriptExecutor, params string[] args)
        {
            if (!CorrectArgumentsCount(1, args))
                new CommandResult(ExecutedCommandStatus.Error, "Wrong arguments count.");

            var scriptPath = args[0];

            if (File.Exists(scriptPath))
            {
                var script = File.ReadAllText(scriptPath);
                var scriptResult = await scriptExecutor.ExecuteAsync(script);

                return new CommandResult(ExecutedCommandStatus.Success, PrepareMessage(scriptResult));
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
            return null;
        }
    }
}
