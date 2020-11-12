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
    public class GenresController : ControllerBase
    {
        private readonly IGenreService genreService;

        public GenresController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        //GET api/genres
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            IEnumerable<GenreDTO> genres;
            try
            {
                genres = await this.genreService.GetAllGenres();
            }
            catch (Exception)
            {
                return NotFound();
            }
            return Ok(genres);
        }

        //GET api/genres/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            GenreDTO genre;
            try
            {
                genre = await this.genreService.GetGenreById(id);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return Ok(genre);
        }

        //POST api/genres
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] GenreViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var genreDTO = new GenreDTO
            {
                Id = model.Id,
                Name = model.Name
            };

            GenreDTO genre;
            try
            {
                genre = await this.genreService.Create(genreDTO);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Created("post", genre);
        }

        //PUT api/genres/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] GenreViewModel model)
        {
            if (id < 1 || model == null)
            {
                return BadRequest("Invalid input");
            }

            var genreDTO = new GenreDTO
            {
                Id = model.Id,
                Name = model.Name
            };

            GenreDTO updatedGenreDTO;
            try
            {
                updatedGenreDTO = await this.genreService.Update(id, genreDTO);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return Ok(updatedGenreDTO);
        }

        //DELETE api/songs/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await this.genreService.Delete(id);

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