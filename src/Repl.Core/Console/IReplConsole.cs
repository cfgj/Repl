using System;

namespace Repl.Core.Console
{
    public interface IReplConsole
    {
        void Write(string value);

        void WriteLine(string value);

        string ReadLine();

        void Exit();

        void ResetColor();

        ConsoleColor ForegroundColor { get; set; }
    }
}
