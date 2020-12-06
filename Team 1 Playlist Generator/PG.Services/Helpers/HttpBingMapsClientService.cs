using PG.Services.Contracts.Helpers;
using System.Net.Http;
using System.Threading.Tasks;

namespace PG.Services.Helpers
{
    public class HttpBingMapsClientService : IHttpBingMapsClientService
    {
        private readonly HttpClient _httpClient;

        public HttpBingMapsClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetAsync(string uri)
            => await _httpClient.GetAsync(uri);
    }
}
