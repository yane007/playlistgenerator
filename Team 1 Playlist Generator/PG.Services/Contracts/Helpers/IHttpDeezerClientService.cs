using System.Net.Http;
using System.Threading.Tasks;

namespace PG.Services.Contracts.Helpers
{
    public interface IHttpDeezerClientService
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}