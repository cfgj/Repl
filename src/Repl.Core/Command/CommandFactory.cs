using System;
using System.Collections.Generic;
using System.Linq;

namespace Repl.Core.Command
{
    public class CommandFactory : ICommandFactory
    {
        public IDictionary<string, ICommand> _commands;

        public CommandFactory()
        {
            _commands = new Dictionary<string, ICommand>();
        }

        public void RegisterCommand(ICommand command)
        {
            if (_commands.ContainsKey(command.Name))
                throw new Exception($"Command \"{command.Name}\" is already registered.");
            _commands.Add(command.Name, command);
        }

        public ICommand GetCommand(string name)
        {
            var command = _commands.Where(c => c.Key == name).Select(c => c.Value).FirstOrDefault();
            if (command == null)
                throw new Exception($"Command \"{name}\" is not registered.");
            return command;
        }
    }
}
