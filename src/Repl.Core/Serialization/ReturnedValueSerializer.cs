using Newtonsoft.Json;

namespace Repl.Core.Serialization
{
    public class ReturnedValueSerializer : IReturnedValueSerializer
    {
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects
            });
        }
    }
}
