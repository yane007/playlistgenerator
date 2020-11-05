using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PG.Data.Context;
using PG.Models;
using PG.Services;
using PG.Services.MappingModelsAPI;
using PG.Web.Models;

namespace PG.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly PGDbContext _context;
        private readonly DeezerAPIService _apiService;

        public HomeController(PGDbContext context, DeezerAPIService apiService)
        {
            _context = context;
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {

            await GetAlbumAsync();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




        [NonAction]
        public async Task GetAlbumAsync()
        {
            await _apiService.ExtractSongsFromPlaylists("pop");
            await _apiService.ExtractSongsFromPlaylists("rock");
        }


        class Playlist1
        {
            public int id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public int duration { get; set; }
            public bool is_loved_track { get; set; }
            public bool collaborative { get; set; }
            public int nb_tracks { get; set; }
            public int fans { get; set; }
            public string link { get; set; }
            public string share { get; set; }
            public string picture { get; set; }
            public string picture_small { get; set; }
            public string picture_medium { get; set; }
            public string picture_big { get; set; }
            public string picture_xl { get; set; }
            public string checksum { get; set; }
            public string tracklist { get; set; }
            public string creation_date { get; set; }
            public string md5_image { get; set; }
            public Creator1 creator { get; set; }
            public string type { get; set; }
            public Tracks1 tracks { get; set; }
        }
        class Creator1
        {
            public int id { get; set; }
            public string name { get; set; }
            public string tracklist { get; set; }
            public string type { get; set; }
        }
        class Tracks1
        {
            public List<Song1> data { get; set; }
            public string checksum { get; set; }
        }
        class Song1
        {
            public int id { get; set; }
            public bool readable { get; set; }
            public string title { get; set; }
            public string title_short { get; set; }
            public string title_version { get; set; }
            public string link { get; set; }
            public int duration { get; set; }
            public int rank { get; set; }
            public bool explicit_lyrics { get; set; }
            public int explicit_content_lyrics { get; set; }
            public int explicit_content_cover { get; set; }
            public string preview { get; set; }
            public string md5_image { get; set; }
            public int time_add { get; set; }
            public Creator1 artist { get; set; }
            public string type { get; set; }
        }
        class Album1
        {
            public int id { get; set; }
            public string title { get; set; }
            public string cover { get; set; }
            public string tracklist { get; set; }

            public Genressssss1 genres { get; set; }

            public Tracks1 tracks { get; set; }
            public string type { get; set; }
        }
        class Genressssss1
        {
            public List<Genre1> data { get; set; }
        }
        class Genre1
        {
            public int id { get; set; }
            public string name { get; set; }
            public string picture { get; set; }
        }
    }
}
