using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Repl.Command;
using Repl.Core.Command;
using Repl.Core.Commands;
using Repl.Core.Console;
using Repl.Core.Engine;
using Repl.Core.Serialization;

namespace Repl
{
    public class Repl
    {
        protected readonly IReplConsole _console;
        protected readonly IScriptExecutor _scriptExecutor;
        protected readonly IReturnedValueSerializer _serializer;
        protected readonly IComponentContext _componentContext;

        protected readonly CommandParser _commandParser;
        protected readonly IDictionary<string, ICommand> _commands;
        protected readonly CommandContext _commandContext;

        public Repl(IReplConsole console,
                    IScriptExecutor scriptExecutor,
                    IReturnedValueSerializer serializer,
                    IComponentContext componentContext)
        {
            _console = console;
            _scriptExecutor = scriptExecutor;
            _serializer = serializer;
            _componentContext = componentContext;

            var commands = new List<ICommand>
            {
                //_componentContext.Resolve<IAddReferenceCommand>(),
                _componentContext.Resolve<ILoadScriptCommand>(),
                _componentContext.Resolve<IResetExecutionEnvironmentCommand>(),
                _componentContext.Resolve<IHelpCommand>()
            };
            _commands = commands.ToDictionary(c => c.Name);

            _commandParser = new CommandParser();
            _commandContext = new CommandContext(_scriptExecutor, _console, _commands);

            Buffer = string.Empty;
        }

        #region Public methods

        public string Buffer { get; protected set; }

        public async Task OnAsync()
        {
            _console.WriteLine("C# Repl (type #help for help) " + Environment.NewLine);

            while (await ExecuteLineAsync()) { }
        }

        #endregion

        #region Private methods

        private async Task<bool> ExecuteLineAsync()
        {
            _console.Write(string.IsNullOrEmpty(Buffer) ? " > " : " : ");
            Buffer += _console.ReadLine();
            try
            {
                if (IsCommand(Buffer))
                {
                    await ExecuteCommand();
                }
                else
                {
                    await ExecuteScriptAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                Debugger.Break();
                return false;
            }
        }

        private bool IsCommand(string line)
        {
            return line.StartsWith("#");
        }

        private async Task ExecuteCommand()
        {
            var commandInfo = _commandParser.Parse(Buffer);

            try
            {
                var command = _commands.Where(c => c.Key == commandInfo.CommandName).Select(c => c.Value).FirstOrDefault();

                if (command == null)
                {
                    _console.ForegroundColor = ConsoleColor.Red;
                    _console.WriteLine($"Command \"{commandInfo.CommandName}\" does not exist.");
                    _console.ResetColor();
                }
                else
                {
                    var commandResult = await command.ExecuteAsync(_commandContext, commandInfo.CommandArgs);
                    WriteCommandResult(commandResult);
                }
            }
            catch (Exception ex)
            {
                _console.ForegroundColor = ConsoleColor.Red;
                _console.WriteLine(ex.Message);
                _console.ResetColor();
            }
            finally
            {
                Buffer = string.Empty;
            }
        }

        public void WriteCommandResult(CommandResult result)
        {
            if (!string.IsNullOrWhiteSpace(result.Message))
            {
                if (result.Status == ExecutedCommandStatus.Success)
                {
                    _console.ForegroundColor = ConsoleColor.Green;
                    _console.Write("Success");
                }
                else if (result.Status == ExecutedCommandStatus.Error)
                {
                    _console.ForegroundColor = ConsoleColor.Red;
                    _console.Write("Error");
                }

                _console.Write(": ");
                _console.WriteLine(result.Message);

                _console.ResetColor();
            }
        }

        private async Task ExecuteScriptAsync()
        {
            var result = await _scriptExecutor.ExecuteAsync(Buffer);
            if (!result.IsComplete)
            {
                Buffer += Environment.NewLine;
            }
            else
            {
                WriteScriptResult(result);
                Buffer = string.Empty;
            }
        }

        private void WriteScriptResult(IScriptResult result)
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
                _console.WriteLine("=> " + _serializer.Serialize(result.ReturnedValue));
            }
            _console.ResetColor();
        }

        #endregion
    }
}
