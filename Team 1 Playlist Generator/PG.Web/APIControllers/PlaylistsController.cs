using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PG.Models;
using PG.Services.Contract;
using PG.Services.Mappers;
using PG.Web.APIControllers.Models;
using PG.Web.APIControllers.Models.Playlist;
using PG.Web.Models;
using PG.Web.Models.Mappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Web.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Identity.Application," + JwtBearerDefaults.AuthenticationScheme)]
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;
        private readonly IBingMapsAPIService _bingMapsAPIService;
        private readonly UserManager<User> _userManager;

        public PlaylistsController(IPlaylistService playlistService, IBingMapsAPIService bingMapsAPIService, UserManager<User> userManager)
        {
            _playlistService = playlistService;
            _bingMapsAPIService = bingMapsAPIService;
            _userManager = userManager;
        }

        //GET api/playlists
        [SwaggerOperation(Description = "`Gets all playlists`")]
        [HttpGet]
        public async Task<IActionResult> GetPlaylists()
        {
            var playlists = await _playlistService.GetAllPlaylists();
            var playlistsViewModels = playlists.Select(x => x.ToViewModel());

            return Ok(playlistsViewModels);
        }

        //GET api/playlists/id
        [SwaggerOperation(Description = "`Gets a playlist by ID`")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlaylistDetails(int id)
        {
            var playlist = await _playlistService.GetPlaylistById(id);
            var playlistViewModel = playlist.ToViewModel();

            return Ok(playlistViewModel);
        }

        //POST api/playlists
        [SwaggerOperation(Description = "`Generates a playlsit based on parameters`")]
        [HttpPost]
        public async Task<IActionResult> GeneratePlaylist(PlaylistGeneratorAPI formInput)
        {
            int tripTime = await _bingMapsAPIService.FindDuration(formInput.StartLocation, formInput.EndLocation);
            var user = await _userManager.GetUserAsync(User);

            await _playlistService.GeneratePlaylist(tripTime, formInput.PlaylistName,
                formInput.Metal, formInput.Rock, formInput.Pop, formInput.Chalga, formInput.TopTracks, formInput.SameArtist, user);

            return Ok();
        }

        //PUT api/playlists/id
        [SwaggerOperation(Description ="`Update a playlist by ID`")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlaylist(int id, [FromBody] PlaylistUpdateAPI model)
        {
            var playlistToUpdate = new PlaylistViewModel 
            {
                Title = model.Title,
                PixabayImage = model.PixabayImage,
                IsPublic = model.IsPublic
            };

            var playlist = await _playlistService.Update(id, playlistToUpdate.ToDTO());
            var playlistViewModel = playlist.ToViewModel();

            return Ok(playlistViewModel);
        }

        //DELETE api/playlists/id
        [SwaggerOperation(Description = "`Delete a playlist by ID`")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylist(int id)
        {
            await _playlistService.Delete(id);

            return Ok();
        }


    }
}
