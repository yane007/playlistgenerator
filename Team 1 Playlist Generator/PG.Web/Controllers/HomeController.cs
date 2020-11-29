using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PG.Data.Context;
using PG.Models;
using PG.Services.Contract;
using PG.Services.DTOs;
using PG.Web.Models;
using PG.Web.Models.Mappers;
using Serilog;

namespace PG.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly PGDbContext _context;
        private readonly IPlaylistService _playlistService;
        private readonly IDeezerAPIService _apiService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public HomeController(PGDbContext context, IDeezerAPIService apiService, IPlaylistService playlistService,
            UserManager<User> userManager, SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _apiService = apiService;
            _playlistService = playlistService;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            Log.Logger.Information("- Getting index page -");
            await GetAlbumAsync();


            //TODO: where to move? 
            //await _roleManager.CreateAsync(new IdentityRole("user"));
            //await _roleManager.CreateAsync(new IdentityRole("admin"));


            //TODO: take top 3 playlists
            IEnumerable<PlaylistDTO> playlistsDTOs = await _playlistService.GetAllPlaylists();
            IList<PlaylistViewModel> playlistsViewModels = playlistsDTOs.Take(3).Select(x => x.ToViewModel()).ToList();

            return View(playlistsViewModels);
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
            await _apiService.ExtractSongsFromGenre("pop");
            await _apiService.ExtractSongsFromGenre("rock");
            await _apiService.ExtractSongsFromGenre("metal");
            await _apiService.ExtractSongsFromGenre("chalga");
        }

    }
}
