using System.Collections.Generic;

namespace Repl.Core.Preprocessors
{
    public class PreprocessedScript
    {
        public PreprocessedScript(string script, IEnumerable<string> imports)
        {
            Script = script;
            Imports = imports ?? new List<string>();
        }

        public string Script { get; private set; }

        public IEnumerable<string> Imports { get; private set; }
    }
}
