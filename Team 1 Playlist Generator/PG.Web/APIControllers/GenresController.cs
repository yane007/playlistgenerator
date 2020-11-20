using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PG.Services.Contract;
using PG.Web.Models;
using PG.Web.Models.Mappers;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Web.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Identity.Application," + JwtBearerDefaults.AuthenticationScheme)]
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

            var genresViewModels = genres.Select(x => x.ToViewModel());

            return Ok(genresViewModels);
        }

        //GET api/genres/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var genre = await _genreService.GetGenreById(id);

            var genreViewModel = genre.ToViewModel();

            return Ok(genreViewModel);
        }

        //POST api/genres
        [HttpPost("")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateGenre(GenreViewModel model)
        {
            var genre = await _genreService.Create(model.ToDTO());

            var genreViewModel = genre.ToViewModel();

            return Created("post", genreViewModel);
        }

        //PUT api/genres/id
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreViewModel model)
        {
            var genre = await _genreService.Update(id, model.ToDTO());

            var genreViewModel = genre.ToViewModel();

            return Ok(genreViewModel);
        }

        //DELETE api/songs/id
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            await _genreService.Delete(id);

            return Ok();
        }
    }
}