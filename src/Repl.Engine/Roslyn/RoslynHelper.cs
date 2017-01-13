using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Repl.Engine.Roslyn
{
    public static class RoslynHelper
    {
        public static CSharpParseOptions ParseOptions => new CSharpParseOptions(LanguageVersion.CSharp6, DocumentationMode.Parse, SourceCodeKind.Script, null);

        public static SyntaxTree GetSyntaxTree(string script)
        {
            return CSharpSyntaxTree.ParseText(script, ParseOptions);
        }
    }
}
