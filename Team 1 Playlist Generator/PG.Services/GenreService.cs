using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PG.Data.Context;
using PG.Models;
using PG.Services.Contract;
using PG.Services.Contracts.Helpers;
using PG.Services.DTOs;
using PG.Services.Mappers;
using PG.Services.MappingModelsAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Services
{
    public class GenreService : IGenreService
    {
        private readonly PGDbContext _context;
        private readonly IHttpDeezerClientService _httpDeezerClientService;
        private readonly IArtistService _artistService;
        private readonly ISongService _songService;

        public GenreService(
            PGDbContext context,
            IArtistService artistService,
            ISongService songService,
            IHttpDeezerClientService httpDeezerClientService)
        {
            _context = context;
            _artistService = artistService;
            _songService = songService;
            _httpDeezerClientService = httpDeezerClientService;
        }


        public async Task<GenreDTO> Create(GenreDTO genreDTO)
        {
            if (genreDTO == null)
            {
                throw new ArgumentNullException("Null Genre");
            }
            if (genreDTO.Name.Length > 50)
            {
                throw new ArgumentOutOfRangeException("Genre's Name needs to be shorter than 50 characters.");
            }

            var existingGenre = await _context.Genres.FirstOrDefaultAsync(x => x.Name == genreDTO.Name);
            if (existingGenre != null)
            {
                throw new ArgumentException($"Genre with name '{genreDTO.Name}' already exists.");
            }

            var genre = _context.Genres.Add(genreDTO.ToEntity());
            await _context.SaveChangesAsync();

            return genre.Entity.ToDTO();
        }

        public async Task Delete(int id)
        {
            var expectedGenre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);
            if (expectedGenre == null)
            {
                throw new ArgumentNullException($"Song with id {id} was not found.");
            }
            if (expectedGenre.IsDeleted)
            {
                throw new ArgumentException($"Song with id {id} is already deleted.");
            }

            expectedGenre.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GenreDTO>> GetAllGenres()
        {
            return await _context.Genres.Where(x => !x.IsDeleted)
                                  .Select(x => x.ToDTO())
                                  .ToListAsync();
        }

        public async Task<GenreDTO> GetGenreById(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (genre == null)
            {
                throw new ArgumentNullException($"Genre with id {id} was not found.");
            }

            return genre.ToDTO();
        }

        public async Task SyncGenresAsync()
        {
            var dbGenres = await GetAllGenres();

            foreach (var genre in dbGenres)
            {
                string playlistsUri = $"search/playlist?q={genre.Name}";

                var playlistsResponse = await _httpDeezerClientService.GetAsync(playlistsUri);
                var playlistsResponseResult = await playlistsResponse.Content.ReadAsStringAsync();
                var deezerPlaylists = JsonConvert.DeserializeObject<QueryPlaylistsAPI>(playlistsResponseResult);

                if (deezerPlaylists.Data == null)
                {
                    continue;
                }

                foreach (var playlist in deezerPlaylists.Data)
                {
                    string playlistUri = playlist.Tracklist.Substring(23);

                    var playlistResponse = await _httpDeezerClientService.GetAsync(playlistUri);
                    var playlistResponseResult = await playlistResponse.Content.ReadAsStringAsync();
                    var deezerPlaylist = JsonConvert.DeserializeObject<PlaylistAPI>(playlistResponseResult);

                    if (deezerPlaylist.Data == null)
                    {
                        continue;
                    }

                    foreach (var song in deezerPlaylist.Data)
                    {
                        if (song.Preview == null || song.Preview.Length < 5)
                        {
                            continue;
                        }

                        var dbsong = await _context.Songs.FirstOrDefaultAsync(x => x.Title == song.Title && !x.IsDeleted);
                        if (dbsong == null)
                        {
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

                            var dbAlbum = await _context.Albums.FirstOrDefaultAsync(x => x.Title == song.Album.Title && !x.IsDeleted);
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

                            var songToAdd = new Song()
                            {
                                Title = song.Title,
                                DeezerID = song.Id,
                                Duration = song.Duration,
                                Rank = song.Rank,
                                Preview = song.Preview,
                                Link = song.Link,
                                GenreId = genre.Id,
                                ArtistId = dbArtist.Id,
                                AlbumId = dbAlbum.Id
                            };

                            await _songService.Create(songToAdd.ToDTO());

                            continue;
                        }

                        if (dbsong.Title != song.Title || dbsong.Preview != song.Preview)
                        {
                            var updatedSongData = new SongDTO
                            {
                                DeezerID = dbsong.DeezerID,
                                Title = dbsong.Title,
                                Duration = dbsong.Duration,
                                Rank = dbsong.Rank,
                                Preview = dbsong.Preview,
                            };

                            await _songService.Update(dbsong.Id, updatedSongData);
                        }
                    }

                    await _context.SaveChangesAsync();

                    System.Threading.Thread.Sleep(150);
                }
            }

        }

        public async Task<GenreDTO> Update(int id, GenreDTO genreDTO)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (genre == null)
            {
                throw new ArgumentNullException($"Genre with id {id} was not found.");
            }

            genre.Name = genreDTO.Name;

            await _context.SaveChangesAsync();

            return genre.ToDTO();
        }
    }
}
