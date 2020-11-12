﻿using Microsoft.AspNetCore.Mvc;
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
    public class SongsController : ControllerBase
    {
        private readonly ISongService songService;

        public SongsController(ISongService songService)
        {
            this.songService = songService;
        }

        //GET api/songs
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            IEnumerable<SongDTO> songs;
            try
            {
                songs = await this.songService.GetAllSongs();
            }
            catch (Exception)
            {
                return NotFound();
            }
            return Ok(songs);
        }

        //GET api/songs/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            SongDTO song;
            try
            {
                song = await this.songService.GetSongById(id);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return Ok(song);
        }

        //POST api/songs
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] SongViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var songDTO = new SongDTO
            {
                Id = model.Id,
                Title = model.Title,
                Link = model.Link,
                Duration = model.Duration,
                Rank = model.Rank,
                Preview = model.Preview,
                ArtistId = model.ArtistId,
                GenreId = model.GenreId
            };

            SongDTO song;
            try
            {
                song = await this.songService.Create(songDTO);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Created("post", song);
        }

        //PUT api/songs/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SongViewModel model)
        {
            if (id < 1 || model == null)
            {
                return BadRequest("Invalid input");
            }

            var songDTO = new SongDTO
            {
                Id = model.Id,
                Title = model.Title,
                Link = model.Link,
                Duration = model.Duration,
                Rank = model.Rank,
                Preview = model.Preview,
                ArtistId = model.ArtistId,
                GenreId = model.GenreId
            };

            SongDTO updatedSongDTO;
            try
            {
                updatedSongDTO = await this.songService.Update(id, songDTO);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return Ok(updatedSongDTO);
        }

        //DELETE api/songs/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await this.songService.Delete(id);

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