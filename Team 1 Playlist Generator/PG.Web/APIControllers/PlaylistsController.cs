using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using PG.Services.Contract;
using PG.Services.DTOs;
using PG.Services.Mappers;
using PG.Web.Models;
using PG.Web.Models.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Web.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlaylistService playlistService;

        public PlaylistsController(IPlaylistService playlistService)
        {
            this.playlistService = playlistService;
        }

        //GET api/playlists
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            IEnumerable<PlaylistDTO> playlists;
            try
            {
                playlists = await this.playlistService.GetAllPlaylists();
            }
            catch (Exception)
            {
                return NotFound();
            }
            return Ok(playlists);
        }

        //GET api/playlists/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            PlaylistDTO playlist;
            try
            {
                playlist = await this.playlistService.GetPlaylistById(id);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return Ok(playlist);
        }

        //POST api/playlists
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] PlaylistViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var playlistDTO = new PlaylistDTO
            {
                Title = model.Title,
                Description = model.Description,
                Duration = model.Duration,
                Fans = model.Fans,
                Link = model.Link,
                Share = model.Share,
                Picture = model.Picture,
                Tracklist = model.Tracklist,
                Creation_date = model.Creation_date,
                Type = model.Type,
                PlaylistsSongs = model.PlaylistsSongs,
            };

            PlaylistDTO playlist;
            try
            {
                playlist = await this.playlistService.Create(playlistDTO);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Created("post", playlist);
        }

        //PUT api/playlists/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PlaylistViewModel model)
        {
            if (id < 1 || model == null)
            {
                return BadRequest("Invalid input");
            }

            var playlistDTO = new PlaylistDTO
            {
                Title = model.Title,
                Description = model.Description,
                Duration = model.Duration,
                Fans = model.Fans,
                Link = model.Link,
                Share = model.Share,
                Picture = model.Picture,
                Tracklist = model.Tracklist,
                Creation_date = model.Creation_date,
                Type = model.Type,
                PlaylistsSongs = model.PlaylistsSongs,
            };

            PlaylistDTO updatedPlaylistDTO;
            try
            {
                updatedPlaylistDTO = await this.playlistService.Update(id, playlistDTO);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return Ok(updatedPlaylistDTO);
        }

        //DELETE api/playlists/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await this.playlistService.Delete(id);

            if (result == true)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
