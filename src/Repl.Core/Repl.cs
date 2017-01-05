using System;
using System.Threading.Tasks;
using Repl.Core.Console;
using Repl.Core.Engine;

namespace Repl.Core
{
    public class Repl : IRepl
    {
        protected IReplConsole _console;

        protected IScriptExecutor _scriptExecutor;

        public Repl(IReplConsole console, IScriptExecutor scriptExecutor)
        {
            _console = console;
            _scriptExecutor = scriptExecutor;
        }

        public async Task OnAsync()
        {
            while (await ExecuteLineAsync()) { }
        }

        private async Task<bool> ExecuteLineAsync()
        {
            _console.Write(" > ");
            var line = _console.ReadLine();
            try
            {
                var result = await _scriptExecutor.ExecuteAsync(line);
                WriteResult(result);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void WriteResult(IScriptResult result)
        {
            if (result.CompilationFailed)
            {
                _console.ForegroundColor = ConsoleColor.Red;
                _console.WriteLine(result.CompilationException.Message);
            }
            else if (result.ExecutionFailed)
            {
                _console.ForegroundColor = ConsoleColor.Red;
                _console.WriteLine(result.ExecutionException.Message);
            }
            else if (result.ReturnedValue != null)
            {
                _console.ForegroundColor = ConsoleColor.Green;
                _console.WriteLine("=> " + result.ReturnedValue.ToString());
            }
            _console.ResetColor();
        }
    }
}
