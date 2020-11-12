using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DeezerApiData.Models.BingApi;
using DeezerApiData.Models.BingApi.LocationBingApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PG.Services.Contract;
using PG.Services.DTOs;
using PG.Web.Models;
using PG.Web.Models.Mappers;

namespace PG.Web.Controllers
{
    public class PlaylistsController : Controller
    {
        private readonly IPlaylistService _playlistService;
        private readonly IGenreService _genreService;

        public PlaylistsController(IPlaylistService playlistService, IGenreService genreService)
        {
            this._playlistService = playlistService;
            this._genreService = genreService;

        }
        public async Task<IActionResult> Index()
        {
            var playlistsDTOs = await _playlistService.GetAllPlaylists();

            var playlistsViewModels = playlistsDTOs.Select(x => x.ToViewModel());

            await _playlistService.GeneratePlaylist(new PlaylistDTO() { Title = "The 5 song playlist"});

            return View(playlistsViewModels);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("Login.aspx");
            }

            var genresDTOs = await _genreService.GetAllGenres();

            var genresViewModels = genresDTOs.Select(x => x.ToViewModel());

            return View(genresViewModels);
        }
        public IActionResult FindDuration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FindDuration(JourneyViewModel model)
        {
            var startingUrl = $"https://dev.virtualearth.net/REST/v1/Locations/{model.StartLocation}?key=AqYHIDdNVo4xufKpBNAuFfBrGtqJw_fJm45HlPU25Mrc-YSBHO8VcDK5zuHaXt4D";
            var endingUrl = $"https://dev.virtualearth.net/REST/v1/Locations/{model.EndLocation}?key=AqYHIDdNVo4xufKpBNAuFfBrGtqJw_fJm45HlPU25Mrc-YSBHO8VcDK5zuHaXt4D";

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

            return View(distance);
        }
    }
}
