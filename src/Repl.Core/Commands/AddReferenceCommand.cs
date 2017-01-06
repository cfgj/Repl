using System;
using System.Threading.Tasks;
using Repl.Core.Command;

namespace Repl.Core.Commands
{
    public interface IAddReferenceCommand : ICommand { }

    public class AddReferenceCommand : CommandBase, IAddReferenceCommand
    {
        public override string Name
        {
            get { return "r"; }
        }

        public override int ArgumentsNumber
        {
            get { return 1; }
        }

        public override string Description
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        protected override Task<CommandResult> InternalExecuteAsync(CommandContext context, params string[] args)
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
