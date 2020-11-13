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
    public class APISongsController : ControllerBase
    {
        private readonly ISongService _songService;

        public APISongsController(ISongService songService)
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
        public async Task<IActionResult> Create([FromBody] SongViewModel model)
        {
            var song = await _songService.Create(model.ToDTO());

            return Created("post", song.ToViewModel());
        }

        //PUT api/songs/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SongViewModel model)
        {
            var song = await _songService.Update(id, model.ToDTO());

            return Ok(song);
        }

        //DELETE api/songs/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _songService.Delete(id);

            return Ok();
        }
    }
}