using System;

namespace Repl.Core.Console
{
    using C = System.Console;

    public class ReplConsole : IReplConsole
    {
        public void Write(string value)
        {
            C.Write(value);
        }

        public void Write(string value, ConsoleColor color)
        {
            C.BackgroundColor = color;
            Write(value);
            ResetColor();
        }

        public void Write(string format, params object[] args)
        {
            C.Write(format, args);
        }

        public void WriteLine(string value)
        {
            C.WriteLine(value);
        }

        public void WriteLine(string value, ConsoleColor color)
        {
            C.ForegroundColor = color;
            WriteLine(value);
            ResetColor();
        }

        public void WriteLine(string format, params object[] args)
        {
            C.WriteLine(format, args);
        }

        public string ReadLine()
        {
            return C.ReadLine();
        }

        public void Exit()
        {
            ResetColor();
        }

        public void ResetColor()
        {
            C.ResetColor();
        }

        public ConsoleColor ForegroundColor
        {
            get { return C.ForegroundColor; }
            set { C.ForegroundColor = value; }
        }
    }
}
