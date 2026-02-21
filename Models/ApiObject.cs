using System.Text.Json.Serialization;

namespace RestfulApiTests.Models
{
    public class ApiObject
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("data")]
        public object Data { get; set; }
    }
}