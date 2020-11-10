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
    }
}
//TODO