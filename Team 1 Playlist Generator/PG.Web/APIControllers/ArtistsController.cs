using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    [Authorize(AuthenticationSchemes = "Identity.Application," + JwtBearerDefaults.AuthenticationScheme)]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        //GET api/artists
        [HttpGet("")]
        public async Task<IActionResult> GetArtists()
        {
            var artists = await _artistService.GetAllArtists();

            var artistsViewModels = artists.Select(x => x.ToViewModel());

            return Ok(artists);
        }

        //GET api/artists/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtistDetails(int id)
        {
            var artist = await _artistService.GetArtistById(id);

            var artistViewModel = artist.ToViewModel();

            return Ok(artistViewModel);
        }

        //POST api/artists
        [HttpPost("")]
        [Authorize(Roles = "admin")]    
        public async Task<IActionResult> CreateArtist(ArtistViewModel model)
        {
            var createdArtist = await _artistService.Create(model.ToDTO());

            var artistViewModel = createdArtist.ToViewModel();

            return Created("post", artistViewModel);
        }

        //PUT api/artists/id
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateArtist(int id, [FromBody] ArtistViewModel artistModel)
        {
            var artist = await _artistService.Update(id, artistModel.ToDTO());

            var artistViewModel = artist.ToViewModel();

            return Ok(artistViewModel);
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