using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Repl.Core.Command;

namespace Repl.Core.Commands
{
    public interface IAddReferenceCommand : ICommand { }

    public class AddReferenceCommand : IAddReferenceCommand
    {
        public string Name
        {
            get { return "r"; }
        }

        public string Description
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IDictionary<string, string> Parameters
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Usage
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Task<CommandResult> ExecuteAsync(CommandContext context, params string[] args)
        {
            // if (args.Length != 1)
            //     new CommandResult(ExecutedCommandStatus.Error, "Wrong arguments count.");
            // var referencePath = args[0];

            // scriptExecutor.AddReference(referencePath);

            // return new CommandResult(ExecutedCommandStatus.Success, $"Reference \"{referencePath}\" added.");
            throw new NotImplementedException();
        }
    }
}
