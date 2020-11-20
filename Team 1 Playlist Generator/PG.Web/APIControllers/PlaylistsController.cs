using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PG.Services.Contract;
using PG.Services.Mappers;
using PG.Web.Models;
using PG.Web.Models.Mappers;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Web.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;

        public PlaylistsController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }

        //GET api/playlists
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var playlists = await _playlistService.GetAllPlaylists();

            var playlistsViewModels = playlists.Select(x => x.ToViewModel());

            return Ok(playlistsViewModels);
        }

        //GET api/playlists/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var playlist = await _playlistService.GetPlaylistById(id);

            var playlistViewModel = playlist.ToViewModel();

            return Ok(playlistViewModel);
        }

        //POST api/playlists
        [HttpPost("")]
        public async Task<IActionResult> CreatePlaylist(PlaylistViewModel model)
        {
            var playlist = await _playlistService.Create(model.ToDTO());

            var playlistViewModel = playlist.ToDTO().ToViewModel();

            return Created("post", playlistViewModel);
        }

        //PUT api/playlists/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlaylist(int id, [FromBody] PlaylistViewModel model)
        {
            var playlist = await _playlistService.Update(id, model.ToDTO());

            var playlistViewModel = playlist.ToViewModel();

            return Ok(playlistViewModel);
        }

        //DELETE api/playlists/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylist(int id)
        {
            await _playlistService.Delete(id);

            return Ok();
        }
    }
}
