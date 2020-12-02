using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using PG.Models;
using PG.Services.Contract;
using PG.Services.DTOs;
using PG.Web.Models;
using PG.Web.Models.Mappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Web.Controllers
{
    [Authorize]
    public class PlaylistsController : Controller
    {
        private readonly IPlaylistService _playlistService;
        private readonly IGenreService _genreService;
        private readonly IBingMapsAPIService _bingMapsAPIService;
        private readonly UserManager<User> _userManager;

        public PlaylistsController(IPlaylistService playlistService, IGenreService genreService, IBingMapsAPIService bingMapsAPIService,
            UserManager<User> userManager)
        {
            _playlistService = playlistService;
            _genreService = genreService;
            _bingMapsAPIService = bingMapsAPIService;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string name = "", string genre = "", string duration = "")
        {
            IEnumerable<PlaylistDTO> playlistsDTOs = await _playlistService.GetAllPlaylistsWithSettings(name, genre, duration);
            IList<PlaylistViewModel> playlistsViewModels = playlistsDTOs
                .Where(x => x.IsPublic)
                .Select(x => x.ToViewModel())
                .ToList();

            var allGenres = await _genreService.GetAllGenres();

            var genresAndDefault = new List<string> { "" };
            genresAndDefault.AddRange(allGenres.Select(x => x.Name));
            ViewBag.Genres = genresAndDefault;

            ViewBag.MinPlaylistDuration = await _playlistService.GetMinPlaylistDuration();
            ViewBag.MaxPlaylistDuration = await _playlistService.GetMaxPlaylistDuration();

            return View(playlistsViewModels);
        }

        public async Task<IActionResult> MyPlaylists()
        {
            IEnumerable<PlaylistDTO> playlistsDTOs = await _playlistService.GetPlaylistsByUser(_userManager.GetUserId(User));
            IList<PlaylistViewModel> playlistsViewModels = playlistsDTOs.Where(x => x.UserId == _userManager.GetUserId(User)).Select(x => x.ToViewModel()).ToList();

            return View(playlistsViewModels);
        }

        public async Task<IActionResult> Create()
        {
            var genresDTOs = await _genreService.GetAllGenres();
            var genresViewModels = genresDTOs.Select(x => x.ToViewModel());
            var playlistGenerator = new PlaylistGeneratorViewModel();

            playlistGenerator.Genres = genresViewModels.ToList();

            return View(playlistGenerator);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlaylistGeneratorViewModel formInput)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("Login.aspx");
            }

            int tripTime = await _bingMapsAPIService.FindDuration(formInput.StartLocation, formInput.EndLocation);
            var user = await _userManager.GetUserAsync(User);

            await _playlistService.GeneratePlaylist(tripTime, formInput.PlaylistName,
                formInput.Metal, formInput.Rock, formInput.Pop, formInput.Chalga, formInput.TopTracks, formInput.SameArtist, user);

            return RedirectToAction("MyPlaylists");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Playlist(int id, int pageNumber = 1)
        {
            PlaylistDTO playlistDTO = await _playlistService.GetPlaylistById(id);
            PlaylistViewModel playlistViewModel = playlistDTO.ToViewModel();

            playlistViewModel.SongsPaged = playlistViewModel.Songs.ToPagedList(pageNumber, 13);

            ViewBag.LoggedUserId = _userManager.GetUserId(User);

            return View(playlistViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePlaylist(int id)
        {
            await _playlistService.Delete(id);

            return RedirectToAction("MyPlaylists");
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePublicAccess(int playlistId, bool isPublic)
        {
            await _playlistService.UpdatePublicAccess(playlistId, isPublic);

            return RedirectToAction("Playlist", new { id = playlistId });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var playlist = await _playlistService.GetPlaylistById(id);


            return View(playlist.ToViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PlaylistViewModel model)
        {
            await _playlistService.Update(model.Id, model.ToDTO());

            return RedirectToAction("MyPlaylists");
        }

        [HttpPost]
        public IActionResult FilterPlaylists(string viewName, string viewGenre, string viewDuration)
        {

            return RedirectToAction("Index",
                new
                {
                    name = viewName,
                    genre = viewGenre,
                    duration = viewDuration,
                });
        }
    }
}

