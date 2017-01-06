using System.Collections.Generic;
using Repl.Core.Console;
using Repl.Core.Engine;

namespace Repl.Core.Command
{
    public class CommandContext
    {
        public CommandContext(IScriptExecutor scriptExecutor, IReplConsole console, IDictionary<string, ICommand> commands)
        {
            ScriptExecutor = scriptExecutor;
            Console = console;
            Commands = commands;
        }

        public IScriptExecutor ScriptExecutor { get; }

        public IReplConsole Console { get; }

        public IDictionary<string, ICommand> Commands { get; }
    }
}
