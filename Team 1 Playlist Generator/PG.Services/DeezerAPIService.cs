using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PG.Data.Context;
using PG.Models;
using PG.Services.Contract;
using PG.Services.MappingModelsAPI;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PG.Services
{
    public class DeezerAPIService : IDeezerAPIService
    {
        private readonly PGDbContext _context;


        public DeezerAPIService(PGDbContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Extract all songs with preview "link".mp3 from all playlists where their title contains "Rock".
        /// Creates new Creator/Artist accordingly to the song's specifications.
        /// </summary>
        public async Task ExtractSongsFromPlaylists(string genreString)
        {
            genreString = genreString.ToLower();
            var dbGenreName = await _context.Genres.FirstOrDefaultAsync(x => x.Name == genreString);

            if (!(dbGenreName == null))
            {
                return;
            }

            var client = new HttpClient();

            using (var response = await client.GetAsync($"https://api.deezer.com/search/playlist?q={genreString}"))
            {
                var responseAsString = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<QueryPlaylistsAPI>(responseAsString);

                var expectedGenre = _context.Genres.FirstOrDefault(x => x.Name.ToLower().Equals(genreString));

                if (expectedGenre == null)
                {
                    await _context.Genres.AddAsync(new Genre() { Name = genreString });
                    await _context.SaveChangesAsync();
                    expectedGenre = await _context.Genres.FirstOrDefaultAsync(a => a.Name.ToLower().Equals(genreString));
                }

                foreach (var playlist in result.Data)
                {
                    using (var response2 = await client.GetAsync(playlist.Tracklist))
                    {
                        var responseAsString2 = await response2.Content.ReadAsStringAsync();

                        var result2 = JsonConvert.DeserializeObject<PlaylistAPI>(responseAsString2);

                        foreach (var song in result2.Data)
                        {
                            if (song.Preview == null || song.Preview.Length < 5)
                            {
                                continue;
                            }

                            var expectedArtist = await _context.Artists.FirstOrDefaultAsync(x => x.Name == song.Artist.Name);
                            if (expectedArtist == null)
                            {
                                var addedArtist = await _context.Artists.AddAsync(new Artist()
                                {
                                    Name = song.Artist.Name,
                                    Tracklist = song.Artist.Tracklist,
                                    Type = song.Artist.Type
                                });
                                expectedArtist = addedArtist.Entity;

                                await _context.SaveChangesAsync();
                            }

                            var expectedAlbum = await _context.Albums.FirstOrDefaultAsync(x => x.Title == song.Album.Title);
                            if (expectedAlbum == null)
                            {
                                var addedAlbum = await _context.Albums.AddAsync(new Album()
                                {
                                    Title = song.Album.Title,
                                    Tracklist = song.Album.Tracklist,
                                });
                                expectedAlbum = addedAlbum.Entity;

                                await _context.SaveChangesAsync();
                            }

                            var isSongNull = await _context.Songs.FirstOrDefaultAsync(x => x.Title == song.Title);
                            if (isSongNull == null)
                            {
                                var songToAdd = new Song()
                                {
                                    Title = song.Title,
                                    DeezerID = song.Id,
                                    Duration = song.Duration,
                                    Rank = song.Rank,
                                    Preview = song.Preview,
                                    Link = song.Link,
                                    GenreId = expectedGenre.Id,
                                    ArtistId = expectedArtist.Id,
                                    AlbumId = expectedAlbum.Id
                                };
                                await _context.Songs.AddAsync(songToAdd);

                                expectedAlbum.Songs.Add(songToAdd);
                            }
                        }

                        await _context.SaveChangesAsync();
                    }

                    System.Threading.Thread.Sleep(150);
                }
            }
        }
    }
}
