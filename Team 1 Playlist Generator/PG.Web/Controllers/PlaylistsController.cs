using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
