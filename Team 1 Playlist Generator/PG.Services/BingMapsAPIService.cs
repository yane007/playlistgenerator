using DeezerApiData.Models.BingApi;
using DeezerApiData.Models.BingApi.LocationBingApi;
using PG.Data.Context;
using PG.Services.Contract;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PG.Services
{
    public class BingMapsAPIService : IBingMapsAPIService
    {
        public BingMapsAPIService()
        {
        }

        public async Task<int> FindDuration(string start, string end)
        {
            HttpClient client = new HttpClient();

            var startCoords = await GetCoords(start, client);
            var endCoords = await GetCoords(end, client);

            int distance = await GetDistance(startCoords, endCoords, client);

            return distance;
        }

        private static async Task<int> GetDistance(string coords, string endCoords, HttpClient client)
        {
            var distanceMatrixDurationUrl = $"https://dev.virtualearth.net/REST/v1/Routes/DistanceMatrix?timeUnit=second&origins={coords}&destinations={endCoords}&travelMode=driving&key=AqYHIDdNVo4xufKpBNAuFfBrGtqJw_fJm45HlPU25Mrc-YSBHO8VcDK5zuHaXt4D";

            HttpResponseMessage response = await client.GetAsync(distanceMatrixDurationUrl);

            var jsonResult = await response.Content.ReadAsStringAsync();

            var distanceResult = JsonSerializer.Deserialize<BingInitialResult>(jsonResult);

            var distance = distanceResult.BingResources.First().TravelResultsCollections.First().Results.First().TravelDuration;
            return distance;
        }

        private async Task<string> GetCoords(string placeURL, HttpClient client)
        {
            placeURL = $"https://dev.virtualearth.net/REST/v1/Locations/{placeURL}?key=AqYHIDdNVo4xufKpBNAuFfBrGtqJw_fJm45HlPU25Mrc-YSBHO8VcDK5zuHaXt4D";

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
