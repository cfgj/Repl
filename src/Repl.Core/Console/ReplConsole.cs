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

        public void WriteLine(string value)
        {
            C.WriteLine(value);
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
