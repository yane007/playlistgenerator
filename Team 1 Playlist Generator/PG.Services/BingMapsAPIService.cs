using DeezerApiData.Models.BingApi;
using DeezerApiData.Models.BingApi.LocationBingApi;
using PG.Data.Context;
using PG.Services.Contract;
using PG.Services.Contracts.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PG.Services
{
    public class BingMapsAPIService : IBingMapsAPIService
    {
        private readonly IHttpBingMapsClientService _httpBingMapsClientService;

        public BingMapsAPIService(IHttpBingMapsClientService httpBingMapsClientService)
        {
            _httpBingMapsClientService = httpBingMapsClientService;
        }

        public async Task<int> FindDuration(string start, string end)
        {
            string startCoords = await GetCoords(start, _httpBingMapsClientService);
            string endCoords = await GetCoords(end, _httpBingMapsClientService);

            int distance = await GetDistance(startCoords, endCoords, _httpBingMapsClientService);

            return distance;
        }

        private static async Task<int> GetDistance(string coords, string endCoords, IHttpBingMapsClientService client)
        {
            var distanceMatrixDurationUrl = $"REST/v1/Routes/DistanceMatrix?timeUnit=second&origins={coords}&destinations={endCoords}&travelMode=driving&key=AqYHIDdNVo4xufKpBNAuFfBrGtqJw_fJm45HlPU25Mrc-YSBHO8VcDK5zuHaXt4D";

            HttpResponseMessage response = await client.GetAsync(distanceMatrixDurationUrl);

            var jsonResult = await response.Content.ReadAsStringAsync();

            var distanceResult = JsonSerializer.Deserialize<BingInitialResult>(jsonResult);

            var distance = distanceResult.BingResources.First().TravelResultsCollections.First().Results.First().TravelDuration;
            return distance;
        }

        private async Task<string> GetCoords(string placeURL, IHttpBingMapsClientService client)
        {
            placeURL = $"REST/v1/Locations/{placeURL}?key=AqYHIDdNVo4xufKpBNAuFfBrGtqJw_fJm45HlPU25Mrc-YSBHO8VcDK5zuHaXt4D";

            HttpResponseMessage response = await client.GetAsync(placeURL);

            string jsonResult = await response.Content.ReadAsStringAsync();

            LocationBingResult resultObject = JsonSerializer.Deserialize<LocationBingResult>(jsonResult);

            IEnumerable<decimal> coords = resultObject
                .LocationResourceCollection.First()
                .LocationResources.First()
                .ResourcePoint.Coordinates;

            return $"{coords.First()},{coords.Last()}";
        }
    }
}
