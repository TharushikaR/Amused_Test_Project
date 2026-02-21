using System.Text;
using System.Text.Json;

namespace RestfulApiTests.Client
{
    public class RestClientHelper
    {
        private readonly HttpClient _client;

        public RestClientHelper()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://restful-api.dev/")
            };
        }

        public async Task<HttpResponseMessage> GetAllObjects()
        {
            return await _client.GetAsync("objects");
        }

        public async Task<HttpResponseMessage> GetObjectById(string id)
        {
            return await _client.GetAsync($"objects/{id}");
        }

        public async Task<HttpResponseMessage> CreateObject(object payload)
        {
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await _client.PostAsync("objects", content);
        }

        public async Task<HttpResponseMessage> UpdateObject(string id, object payload)
        {
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await _client.PutAsync($"objects/{id}", content);
        }

        public async Task<HttpResponseMessage> DeleteObject(string id)
        {
            return await _client.DeleteAsync($"objects/{id}");
        }
    }
}