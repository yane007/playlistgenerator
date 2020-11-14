using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
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
        private readonly IBingMapsAPIService _bingMapsAPIService;

        public PlaylistsController(IPlaylistService playlistService, IGenreService genreService, IBingMapsAPIService bingMapsAPIService)
        {
            _playlistService = playlistService;
            _genreService = genreService;
            _bingMapsAPIService = bingMapsAPIService;
        }
        public async Task<IActionResult> Index()
        {

            return View();
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

            var playlistGenerator = new PlaylistGeneratorViewModel();

            playlistGenerator.Genres = genresViewModels.ToList();


            return View(playlistGenerator);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(PlaylistGeneratorViewModel formInput)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("Login.aspx");
            }

            int tripTime = 10000;
                //await _bingMapsAPIService.FindDuration(formInput.StartLocation, formInput.EndLocation);

            for (int i = 0; i < 1; i++) 
            {
                await _playlistService.GeneratePlaylist(tripTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), 
                    formInput.Metal, formInput.Rock, formInput.Pop, formInput.TopTracks, formInput.SameArtist);

                Thread.Sleep(10);
            }
            
            return RedirectToAction("Index");
        }        
    }
}
