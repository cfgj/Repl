namespace Repl.Core.Command
{
    public class CommandResult
    {
        public CommandResult(ExecutedCommandStatus status, string message = null)
        {
            Status = status;
            Message = message;
        }

        public ExecutedCommandStatus Status { get; private set; }

        public string Message { get; private set; }
    }

    public enum ExecutedCommandStatus : byte
    {
        Success = 1,

        Error
    }
}
