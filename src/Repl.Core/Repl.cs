using System;
using System.Threading.Tasks;
using Repl.Core.Console;
using Repl.Core.Engine;
using Repl.Core.Serialization;

namespace Repl.Core
{
    public class Repl : IRepl
    {
        protected IReplConsole _console;

        protected IScriptExecutor _scriptExecutor;

        protected IReturnedValueSerializer _serilizer;

        public Repl(IReplConsole console, IScriptExecutor scriptExecutor, IReturnedValueSerializer serilizer)
        {
            _console = console;
            _scriptExecutor = scriptExecutor;
            _serilizer = serilizer;

            Buffer = string.Empty;
        }

        public string Buffer { get; protected set; }

        public async Task OnAsync()
        {
            _console.WriteLine("C# Repl " + Environment.NewLine);

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
                _console.WriteLine("=> " + _serilizer.Serialize(result.ReturnedValue));
            }
            _console.ResetColor();
        }
    }
}
