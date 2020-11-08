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
        /// <returns></returns>
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
                 
                //-----------------------------------------------------------------------------------
                //Get genre, if not found we create one. We need its Id for assinging it to a song.
                var expectedGenre = _context.Genres.FirstOrDefault(x => x.Name.ToLower().Equals(genreString));
                if (expectedGenre == null)
                {
                    await _context.Genres.AddAsync(new Genre() { Name = genreString });
                    await _context.SaveChangesAsync();
                    expectedGenre = await _context.Genres.FirstOrDefaultAsync(a => a.Name.ToLower().Equals(genreString));
                }
                //-----------------------------------------------------------------------------------

                foreach (var playlist in result.Data)
                {
                    using (var response2 = await client.GetAsync(playlist.Tracklist))
                    {
                        var responseAsString2 = await response2.Content.ReadAsStringAsync();

                        var result2 = JsonConvert.DeserializeObject<PlaylistAPI>(responseAsString2);

                        //-----------------------------------------------------------------------------------
                        //We start adding a Song to the DB. We need to map SongAPI to Song && check if the CreatorAPI Id is alredy on our DB
                        foreach (var song in result2.Data)
                        {

                            if (song.Preview == null || song.Preview.Length < 5)
                            {
                                continue;
                            }

                            //-----------------------------------------------------------------------------------
                            //Find the Artist, if not exists create new and savechanges
                            var expectedArtist = await _context.Artist.FirstOrDefaultAsync(x => x.Name == song.Artist.Name);
                            if (expectedArtist == null)
                            {
                                await _context.Artist.AddAsync(new Artist()
                                {
                                    Name = song.Artist.Name,
                                    Tracklist = song.Artist.Tracklist,
                                    Type = song.Artist.Type
                                });

                                await _context.SaveChangesAsync();
                                expectedArtist = await _context.Artist.FirstOrDefaultAsync(x => x.Name == song.Artist.Name);
                            }
                            //-----------------------------------------------------------------------------------

                            var isSongNull = await _context.Songs.FirstOrDefaultAsync(x => x.Title == song.Title);
                            if (isSongNull == null)
                            {
                                await _context.Songs.AddAsync(new Song()
                                {

                                    Title = song.Title,
                                    Duration = song.Duration,
                                    Rank = song.Rank,
                                    Preview = song.Preview,

                                    GenreId = expectedGenre.Id,
                                    ArtistId = expectedArtist.Id,
                                });
                                
                            }

                        }
                        await _context.SaveChangesAsync();
                        //-----------------------------------------------------------------------------------
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            }
        }
    }
}
