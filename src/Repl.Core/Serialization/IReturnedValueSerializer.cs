namespace Repl.Core.Serialization
{
    public interface IReturnedValueSerializer
    {
        string Serialize(object obj);
    }
}
