using System;
using System.Threading.Tasks;
using Repl.Core.Command;
using Repl.Core.Engine;

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

        protected override Task<CommandResult> InternalExecuteAsync(IScriptExecutor scriptExecutor, params string[] args)
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
