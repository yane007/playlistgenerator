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
    public class SongsController : ControllerBase
    {
        private readonly ISongService _songService;

        public SongsController(ISongService songService)
        {
            _songService = songService;
        }

        //GET api/songs
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var songs = await _songService.GetAllSongs();

            return Ok(songs);
        }

        //GET api/songs/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var song = await _songService.GetSongById(id);

            return Ok(song.ToViewModel());
        }

        //POST api/songs
        [HttpPost("")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateSong([FromBody] SongViewModel model)
        {
            var song = await _songService.Create(model.ToDTO());

            return Created("post", song.ToViewModel());
        }

        //PUT api/songs/id
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateSong(int id, [FromBody] SongViewModel model)
        {
            var song = await _songService.Update(id, model.ToDTO());

            return Ok(song);
        }

        //DELETE api/songs/id
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            await _songService.Delete(id);

            return Ok();
        }
    }
}