using DeezerApiData.Models.BingApi;
using DeezerApiData.Models.BingApi.LocationBingApi;
using PG.Data.Context;
using PG.Services.Contract;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PG.Services
{
    public class BingMapsAPIService : IBingMapsAPIService
    {
        private readonly PGDbContext _context;


        public BingMapsAPIService(PGDbContext context)
        {
            this._context = context;
        }
        /// <summary>
        /// Finds the travel duration between two locations.
        /// </summary>
        /// <param name="start">First location</param>
        /// <param name="end">Second location</param>
        /// <returns>Returns the travel duration in seconds</returns>
        public async Task<int> FindDuration(string start, string end)
        {
            var startingUrl = $"https://dev.virtualearth.net/REST/v1/Locations/{start}?key=AqYHIDdNVo4xufKpBNAuFfBrGtqJw_fJm45HlPU25Mrc-YSBHO8VcDK5zuHaXt4D";
            var endingUrl = $"https://dev.virtualearth.net/REST/v1/Locations/{end}?key=AqYHIDdNVo4xufKpBNAuFfBrGtqJw_fJm45HlPU25Mrc-YSBHO8VcDK5zuHaXt4D";

            HttpClient client = new HttpClient();

            var response = await client.GetAsync(startingUrl);

            var jsonResult = await response.Content.ReadAsStringAsync();

            var resultObject = JsonSerializer.Deserialize<LocationBingResult>(jsonResult);

            var startingCoordinates = resultObject.LocationResourceCollection.First().LocationResources.First().ResourcePoint.Coordinates;

            response = await client.GetAsync(endingUrl);

            jsonResult = await response.Content.ReadAsStringAsync();

            resultObject = JsonSerializer.Deserialize<LocationBingResult>(jsonResult);

            var endingCoordinates = resultObject.LocationResourceCollection.First().LocationResources.First().ResourcePoint.Coordinates;

            var startCoords = $"{startingCoordinates.First()},{startingCoordinates.Last()}";
            var endCoords = $"{endingCoordinates.First()},{endingCoordinates.Last()}";

            var distanceMatrixDurationUrl = $"https://dev.virtualearth.net/REST/v1/Routes/DistanceMatrix?timeUnit=second&origins={startCoords}&destinations={endCoords}&travelMode=driving&key=AqYHIDdNVo4xufKpBNAuFfBrGtqJw_fJm45HlPU25Mrc-YSBHO8VcDK5zuHaXt4D";

            response = await client.GetAsync(distanceMatrixDurationUrl);

            jsonResult = await response.Content.ReadAsStringAsync();

            var distanceResult = JsonSerializer.Deserialize<BingInitialResult>(jsonResult);

            var distance = distanceResult.BingResources.First().TravelResultsCollections.First().Results.First().TravelDuration;

            return distance;
        }
    }
}
