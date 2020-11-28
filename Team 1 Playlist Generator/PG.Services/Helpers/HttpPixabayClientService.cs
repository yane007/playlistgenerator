using PG.Services.Contracts.Helpers;
using System.Net.Http;
using System.Threading.Tasks;

namespace PG.Services.Helpers //TODO: check
{
    public class HttpPixabayClientService : IHttpPixabayClientService 
    {
        private readonly HttpClient _httpClient;

        public HttpPixabayClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetAsync(string uri)
            => await _httpClient.GetAsync(uri);
    }
}
