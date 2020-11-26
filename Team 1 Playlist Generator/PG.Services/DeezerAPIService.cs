﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PG.Data.Context;
using PG.Models;
using PG.Services.Contract;
using PG.Services.DTOs;
using PG.Services.Mappers;
using PG.Services.MappingModelsAPI;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PG.Services
{
    public class DeezerAPIService : IDeezerAPIService
    {
        private readonly PGDbContext _context;
        private readonly IGenreService _genreService;
        private readonly IArtistService _artistService;
        private readonly ISongService _songService;
        private readonly HttpClient _httpClient;

        public DeezerAPIService(PGDbContext context, IGenreService genreService, IArtistService artistService, ISongService songService)
        {
            _context = context;
            _genreService = genreService;
            _artistService = artistService;
            _songService = songService;
            _httpClient = new HttpClient();
        }


        public async Task ExtractSongsFromPlaylists(string genre)
        {
            genre = genre.ToLower();
            var dbGenre = await _context.Genres.FirstOrDefaultAsync(x => x.Name == genre);

            if (!(dbGenre == null))
            {
                return;
            }

            var playlistsResponse = await _httpClient.GetAsync($"https://api.deezer.com/search/playlist?q={genre}");
            var playlistsResponseResult = await playlistsResponse.Content.ReadAsStringAsync();
            var deezerPlaylists = JsonConvert.DeserializeObject<QueryPlaylistsAPI>(playlistsResponseResult);

            var dbGenreAdded = await _genreService.Create(new GenreDTO { Name = genre});

            foreach (var playlist in deezerPlaylists.Data)
            {

                var playlistResponse = await _httpClient.GetAsync(playlist.Tracklist);
                var playlistResponseResult = await playlistResponse.Content.ReadAsStringAsync();
                var deezerPlaylist = JsonConvert.DeserializeObject<PlaylistAPI>(playlistResponseResult);

                foreach (var song in deezerPlaylist.Data)
                {
                    if (song.Preview == null || song.Preview.Length < 5)
                    {
                        continue;
                    }

                    var dbArtist = await _context.Artists.FirstOrDefaultAsync(x => x.Name == song.Artist.Name);
                    if (dbArtist == null)
                    {
                        var addedArtist = await _artistService.Create(new ArtistDTO()
                        {
                            Name = song.Artist.Name,
                            Tracklist = song.Artist.Tracklist,
                            Type = song.Artist.Type
                        });

                        dbArtist = addedArtist.ToEntity();

                        await _context.SaveChangesAsync();
                    }

                    var dbAlbum = await _context.Albums.FirstOrDefaultAsync(x => x.Title == song.Album.Title);
                    if (dbAlbum == null)
                    {
                        //No service
                        var addedAlbum = await _context.Albums.AddAsync(new Album()
                        {
                            Title = song.Album.Title,
                            Tracklist = song.Album.Tracklist,
                        });
                        dbAlbum = addedAlbum.Entity;

                        await _context.SaveChangesAsync();
                    }

                    var dbSong = await _context.Songs.FirstOrDefaultAsync(x => x.Title == song.Title);
                    if (dbSong == null)
                    {
                        dbSong = new Song()
                        {
                            Title = song.Title,
                            DeezerID = song.Id,
                            Duration = song.Duration,
                            Rank = song.Rank,
                            Preview = song.Preview,
                            //Link = song.Link,
                            GenreId = dbGenreAdded.Id,
                            ArtistId = dbArtist.Id,
                            AlbumId = dbAlbum.Id
                        };

                        await _songService.Create(dbSong.ToDTO());

                        //dbAlbum.Songs.Add(dbSong);

                    } 
                }

                await _context.SaveChangesAsync();
  

                System.Threading.Thread.Sleep(150);
            }
        }
    }
}
