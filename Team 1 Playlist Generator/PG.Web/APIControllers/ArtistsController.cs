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
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService artistService;

        public ArtistsController(IArtistService artistService)
        {
            this.artistService = artistService;
        }

        //GET api/artists
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            IEnumerable<ArtistDTO> artists;
            try
            {
                artists = await this.artistService.GetAllArtists();
            }
            catch (Exception)
            {
                return NotFound();
            }
            return Ok(artists);
        }

        //GET api/artists/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ArtistDTO artist;
            try
            {
                artist = await this.artistService.GetArtistById(id);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return Ok(artist);
        }

        //POST api/artists
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] ArtistViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var artistDTO = new ArtistDTO
            {
                Id = model.Id,
                Name = model.Name,
                Tracklist = model.Tracklist,
                Type = model.Type
            };

            ArtistDTO artist;
            try
            {
                artist = await this.artistService.Create(artistDTO);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Created("post", artist);
        }

        //PUT api/artists/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ArtistViewModel model)
        {
            if (id < 1 || model == null)
            {
                return BadRequest("Invalid input");
            }

            var artistDTO = new ArtistDTO
            {
                Id = model.Id,
                Name = model.Name,
                Tracklist = model.Tracklist,
                Type = model.Type
            };

            ArtistDTO updatedArtistDTO;
            try
            {
                updatedArtistDTO = await this.artistService.Update(id, artistDTO);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return Ok(updatedArtistDTO);
        }

        //DELETE api/artists/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await this.artistService.Delete(id);

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