namespace Repl.Core.Preprocessors
{
    public class ScriptPreprocessor : IScriptPreprocessor
    {
        protected readonly IImportExtractor _importExtractor;

        public ScriptPreprocessor(IImportExtractor importExtractor)
        {
            _importExtractor = importExtractor;
        }

        public virtual PreprocessedScript Process(string script)
        {
            var imports = _importExtractor.GetImports(script);
            return new PreprocessedScript(script, imports);
        }
    }
}
