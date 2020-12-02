using Newtonsoft.Json;
using PG.Services.Contracts.Helpers;
using PG.Services.MappingModelsAPI.Pixabay;
using System.Threading.Tasks;

namespace PG.Services
{
    public class PixabayService : IPixabayService
    {
        private readonly IHttpPixabayClientService _httpPixabayClientService;

        public PixabayService(IHttpPixabayClientService httpPixabayClientService)
        {
            _httpPixabayClientService = httpPixabayClientService;
        }

        public async Task<string> GetPixabayImage(int queryId)
        {
            if (queryId <= 0)
            {
                queryId = 1738;
            }

            string pixabayUri = $"https://pixabay.com/api/?key=19313069-6c4d8d311e25449cde5665c63&id={queryId}&image_type=photo";

            var response = await _httpPixabayClientService.GetAsync(pixabayUri);

            var responseAsString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode || response.ReasonPhrase != "OK")
            {
                return "https://pixabay.com/get/54e2c8454e52b158f6d1877cc4283177083ed8e55559794d7d2a7c_640.jpg";
            }
            var result = JsonConvert.DeserializeObject<PixabayQueryImagesResult>(responseAsString);

            foreach (var image in result.hits)
            {
                if (image == null)
                {
                    continue;
                }
                else if (image.PreviewURL == null || image.WebformatURL == null || image.LargeImageURL == null)
                {
                    continue;
                }
                else
                {
                    return image.WebformatURL;
                }
            }

            return "https://pixabay.com/get/54e2c8454e52b158f6d1877cc4283177083ed8e55559794d7d2a7c_640.jpg";
        }
    }
}
