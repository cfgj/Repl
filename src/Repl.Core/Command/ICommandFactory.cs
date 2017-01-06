namespace Repl.Core.Command
{
    public interface ICommandFactory
    {
        void RegisterCommand(ICommand command);

        ICommand GetCommand(string name);
    }
}
