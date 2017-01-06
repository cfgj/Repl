using System;
using System.Linq;

namespace Repl.Command
{
    public class CommandParser
    {
        public CommandParserResult Parse(string command)
        {
            var segments = command.Split(' ');
            var commandName = segments[0].TrimStart('#');
            var args = new string[0];

            if (segments.Length > 1)
            {
                args = new ArraySegment<string>(segments, 1, segments.Length - 1).ToArray();
            }

            return new CommandParserResult
            {
                CommandName = commandName,
                CommandArgs = args
            };
        }
    }

    public class CommandParserResult
    {
        public string CommandName { get; set; }

        public string[] CommandArgs { get; set; }
    }
}
