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

            return "https://pixabay.com/get/55e5d1444b56a814f1dc846096293e7f1d3cd8ed5b4c704f75297bd29e4ecd5e_640.jpg";
        }
    }
}
