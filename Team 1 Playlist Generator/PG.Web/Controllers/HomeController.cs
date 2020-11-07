using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PG.Data.Context;
using PG.Models;
using PG.Services.Contract;
using PG.Web.Models;

namespace PG.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly PGDbContext _context;
        private readonly IDeezerAPIService _apiService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(PGDbContext context, IDeezerAPIService apiService, 
            UserManager<User> userManager, SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _apiService = apiService;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            await GetAlbumAsync();

            //await _roleManager.CreateAsync(new IdentityRole("user"));
            //await _roleManager.CreateAsync(new IdentityRole("admin"));

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
            await _apiService.ExtractSongsFromPlaylists("metal");
        }

    }
}
