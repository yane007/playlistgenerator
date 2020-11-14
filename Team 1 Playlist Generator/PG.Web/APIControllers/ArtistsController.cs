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
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        //GET api/artists
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var artists = await _artistService.GetAllArtists();

            return Ok(artists);
        }

        //GET api/artists/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var artist = await _artistService.GetArtistById(id);

            return Ok(artist);
        }

        //POST api/artists
        [HttpPost("")]
        [Authorize(Roles = "admin")]    
        public async Task<IActionResult> CreateArtist([FromBody] ArtistViewModel model)
        {
            var createdArtist = await _artistService.Create(model.ToDTO());

            return Created("post", createdArtist);
        }

        //PUT api/artists/id
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateArtist(int id, [FromBody] ArtistViewModel artistModel)
        {
            var artist = await _artistService.Update(id, artistModel.ToDTO());

            return Ok(artist);
        }

        //DELETE api/artists/id
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            await _artistService.Delete(id);

            return Ok();
        }
    }
}