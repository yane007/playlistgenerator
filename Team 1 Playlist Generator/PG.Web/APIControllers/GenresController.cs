using Microsoft.AspNetCore.Mvc;
using PG.Services.Contract;
using PG.Web.Models;
using PG.Web.Models.Mappers;
using System.Threading.Tasks;

namespace PG.Web.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        //GET api/genres
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var genres = await _genreService.GetAllGenres();

            return Ok(genres);
        }

        //GET api/genres/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var genre = await _genreService.GetGenreById(id);

            return Ok(genre);
        }

        //POST api/genres
        [HttpPost("")]
        public async Task<IActionResult> CreateGenre([FromBody] GenreViewModel model)
        {
            var genre = await _genreService.Create(model.ToDTO());

            return Created("post", genre);
        }

        //PUT api/genres/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreViewModel model)
        {
            var genre = await _genreService.Update(id, model.ToDTO());

            return Ok(genre);
        }

        //DELETE api/songs/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            await _genreService.Delete(id);

            return Ok();
        }
    }
}