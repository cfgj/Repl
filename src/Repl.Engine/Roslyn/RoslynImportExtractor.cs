using Microsoft.CodeAnalysis.CSharp.Syntax;
using Repl.Core.Preprocessors;
using System.Collections.Generic;
using System.Linq;

namespace Repl.Engine.Roslyn
{
    public class RoslynImportExtractor : IImportExtractor
    {
        public IEnumerable<string> GetImports(string script)
        {
            var node = RoslynHelper.GetSyntaxTree(script).GetRoot();
            var result = new List<string>();
            var usingDirectives = node.DescendantNodes().OfType<UsingDirectiveSyntax>();

            foreach (var usingDirective in usingDirectives)
            {
                var usingText = usingDirective.Name.GetText().ToString();
                result.Add(usingText);
            }

            return result;
        }
    }
}
