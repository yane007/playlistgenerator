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
            await GetAlbumAsync();

            await _roleManager.CreateAsync(new IdentityRole("user"));
            await _roleManager.CreateAsync(new IdentityRole("admin"));


            IEnumerable<PlaylistDTO> playlistsDTOs = await _playlistService.GetAllPlaylists();

            IEnumerable<PlaylistViewModel> playlistsViewModels = playlistsDTOs.Select(x => x.ToViewModel());

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
            await _apiService.ExtractSongsFromPlaylists("pop");
            //await _apiService.ExtractSongsFromPlaylists("rock");
            //await _apiService.ExtractSongsFromPlaylists("metal");
        }

    }
}
