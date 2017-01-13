using System.Collections.Generic;

namespace Repl.Core.Preprocessors
{
    public interface IImportExtractor
    {
        IEnumerable<string> GetImports(string script);
    }
}
