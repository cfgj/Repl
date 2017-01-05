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

            Buffer = string.Empty;
        }

        public string Buffer { get; protected set; }

        public async Task OnAsync()
        {
            _console.WriteLine("Repl C#" + Environment.NewLine);

            while (await ExecuteLineAsync()) { }
        }

        private async Task<bool> ExecuteLineAsync()
        {
            _console.Write(string.IsNullOrEmpty(Buffer) ? " > " : " : ");
            Buffer += _console.ReadLine();
            try
            {
                var result = await _scriptExecutor.ExecuteAsync(Buffer);
                if (!result.IsComplete)
                {
                    Buffer += Environment.NewLine;
                }
                else
                {
                    WriteResult(result);
                    Buffer = string.Empty;
                }
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
