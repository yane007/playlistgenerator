using PG.Services.Contracts.Helpers;
using System.Net.Http;
using System.Threading.Tasks;

namespace PG.Services.Helpers
{
    public class HttpDeezerClientService : IHttpDeezerClientService
    {
        private readonly HttpClient _httpClient;

        public HttpDeezerClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetAsync(string uri)
            => await _httpClient.GetAsync(uri);
    }
}
