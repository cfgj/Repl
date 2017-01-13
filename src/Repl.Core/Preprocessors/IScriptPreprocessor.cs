namespace Repl.Core.Preprocessors
{
    public interface IScriptPreprocessor
    {
        PreprocessedScript Process(string script);
    }
}
