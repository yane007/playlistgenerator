using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PG.Data.Context;
using PG.Services.Contract;
using PG.Web.Models;

namespace PG.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly PGDbContext _context;
        private readonly IDeezerAPIService _apiService;

        public HomeController(PGDbContext context, IDeezerAPIService apiService)
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
            await _apiService.ExtractSongsFromPlaylists("metal");
        }


    }
}
