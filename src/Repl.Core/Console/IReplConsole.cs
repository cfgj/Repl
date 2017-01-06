using System;

namespace Repl.Core.Console
{
    public interface IReplConsole
    {
        void Write(string value);

        void Write(string value, ConsoleColor color);

        void WriteLine(string value);

        void WriteLine(string value, ConsoleColor color);

        string ReadLine();

        void Exit();

        void ResetColor();

        ConsoleColor ForegroundColor { get; set; }
    }
}
