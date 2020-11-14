using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PG.Services.Contract;
using PG.Services.Mappers;
using PG.Web.Models;
using PG.Web.Models.Mappers;
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

            return Ok(playlists);
        }

        //GET api/playlists/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var playlist = await _playlistService.GetPlaylistById(id);

            return Ok(playlist);
        }

        //POST api/playlists
        [HttpPost("")]
        public async Task<IActionResult> CreatePlaylist([FromBody] PlaylistViewModel model)
        {
            var playlist = await _playlistService.Create(model.ToDTO());

            return Created("post", playlist);
        }

        //PUT api/playlists/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlaylist(int id, [FromBody] PlaylistViewModel model)
        {
            var playlist = await _playlistService.Update(id, model.ToDTO());

            return Ok(playlist);
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
