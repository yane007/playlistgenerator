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
using PG.Web.Models;

namespace PG.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly PGDbContext _context;

        public HomeController(PGDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if ( _context.Genres.FirstOrDefaultAsync().Result == null)
            {
                await this.GetGenresAsync();
            }

            //await GetAlbumAsync();

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
        public async Task GetGenresAsync()
        {
            var client = new HttpClient();

            using (var response = await client.GetAsync("https://api.deezer.com/genre"))
            {
                var responseAsString = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<IndexSourceGenres>(responseAsString);

                await this._context.Genres.AddRangeAsync(result.Genres);

                await this._context.SaveChangesAsync();
            }
        }

        //[NonAction]
        //public async Task GetAlbumAsync()
        //{
        //    var client = new HttpClient();

        //    using (var response = await client.GetAsync("https://api.deezer.com/album/302127"))
        //    {
        //        var responseAsString = await response.Content.ReadAsStringAsync();

        //        var result = JsonConvert.DeserializeObject<Album>(responseAsString);

        //        //foreach (var item in result.Songs)
        //        //{
        //        //    await this._context.Songs.AddRangeAsync(item);

        //        //    await this._context.SaveChangesAsync();
        //        //}


        //    }
        //}
    }
}
