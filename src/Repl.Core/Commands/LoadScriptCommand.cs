using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Repl.Core.Command;
using Repl.Core.Engine;

namespace Repl.Core.Commands
{
    public interface ILoadScriptCommand : ICommand { }

    public class LoadScriptCommand : ILoadScriptCommand
    {
        public string Name
        {
            get { return "load"; }
        }

        public string Description
        {
            get
            {
                return "Loads and executes a script file.";
            }
        }

        public IDictionary<string, string> Parameters
        {
            get
            {
                return new Dictionary<string, string>
                {
                    { "path", "Full path of file." }
                };
            }
        }

        public string Usage
        {
            get
            {
                return "#load [path]";
            }
        }

        public async Task<CommandResult> ExecuteAsync(CommandContext context, params string[] args)
        {
            var scriptPath = args.Count() == 1 ? args[0] : null;

            try
            {
                if (File.Exists(scriptPath))
                {
                    var script = File.ReadAllText(scriptPath);
                    var scriptResult = await context.ScriptExecutor.ExecuteAsync(script);
                    var commandStatus = scriptResult.Success ? ExecutedCommandStatus.Success : ExecutedCommandStatus.Error;

                    return new CommandResult(commandStatus, PrepareMessage(scriptResult));
                }
                else
                {
                    return new CommandResult(ExecutedCommandStatus.Error, string.Format("Script \"{0}\" not found.", scriptPath));
                }
            }
            catch (Exception ex)
            {
                return new CommandResult(ExecutedCommandStatus.Error, ex.Message);
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
