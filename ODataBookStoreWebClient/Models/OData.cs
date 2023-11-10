using Newtonsoft.Json;

namespace ODataBookStoreWebClient.Models
{
    public class OData<T>
    {
        [JsonProperty("@odata.context")]
        public string Metadata { get; set; }
        public T value { get; set; }
    }
}
