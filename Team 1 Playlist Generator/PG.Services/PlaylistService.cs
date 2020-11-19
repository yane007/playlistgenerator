using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PG.Data.Context;
using PG.Models;
using PG.Services.Contract;
using PG.Services.DTOs;
using PG.Services.Mappers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PG.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly PGDbContext _context;

        public PlaylistService(PGDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a playlist
        /// </summary>
        /// <param name="playlistDTO"></param>
        /// <returns></returns>
        public async Task<Playlist> Create(PlaylistDTO playlistDTO)
        {
            if (playlistDTO == null)
            {
                throw new ArgumentNullException("Null Playlist");
            }
            if (playlistDTO.Title.Length > 50)
            {
                throw new ArgumentOutOfRangeException("Playlist's title needs to be shorter than 50 characters.");
            }

            var existingPlaylist = await _context.Playlists.FirstOrDefaultAsync(x => x.Title == playlistDTO.Title);
            if (existingPlaylist != null)
            {
                throw new ArgumentException($"Playlist with title '{playlistDTO.Title}' already exists.");
            }

            Playlist playlistToAdd = playlistDTO.ToEntity();

            var playlist = await _context.Playlists.AddAsync(playlistToAdd);
            Log.Logger.Information($"Playlist with title '{playlist.Entity.Title}' has been created.");

            await _context.SaveChangesAsync();

            return playlist.Entity;
        }

        /// <summary>
        /// Gets all playlsits.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PlaylistDTO>> GetAllPlaylists()
        {
            return await _context.Playlists
                                 .Include(x => x.PlaylistsSongs)
                                 .ThenInclude(x => x.Song)
                                 .Where(x => x.IsDeleted == false)
                                 .Select(x => x.ToDTO())
                                 .ToListAsync();
        }

        /// <summary>
        /// Gets a playlist by user's ID.
        /// </summary>
        /// <param name="userId">User's ID.</param>
        /// <returns></returns>
        public async Task<IEnumerable<PlaylistDTO>> GetPlaylistsByUser(string userId)
        {
            return await _context.Playlists
                                 .Include(x => x.PlaylistsSongs)
                                 .ThenInclude(x => x.Song)
                                 .Where(x => x.UserId == userId && x.IsDeleted == false)
                                 .Select(x => x.ToDTO())
                                 .ToListAsync();
        }

        /// <summary>
        /// Gets playlist by ID.
        /// </summary>
        /// <param name="id">ID of the playlist.</param>
        /// <returns></returns>
        public async Task<PlaylistDTO> GetPlaylistById(int id)
        {
            var playlist = await _context.Playlists
                                 .Include(x => x.PlaylistsSongs)
                                 .ThenInclude(x => x.Song)
                                 .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);

            if (playlist == null)
            {
                throw new ArgumentNullException($"Playlist with id {id} was not found.");
            }

            return playlist.ToDTO();
        }

        /// <summary>
        /// Updates a plalist
        /// </summary>
        /// <param name="id">ID of the playlist to update</param>
        /// <param name="playlistDTO">The new data of the playlist</param>
        /// <returns>Updated playlist to DTO</returns>
        public async Task<PlaylistDTO> Update(int id, PlaylistDTO playlistDTO)
        {
            var playlist = await _context.Playlists.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (playlist == null)
            {
                throw new ArgumentNullException($"Playlist with id {id} was not found.");
            }

            playlist.Title = playlistDTO.Title;
            playlist.Duration = playlistDTO.Duration;
            playlist.Picture = playlistDTO.Picture;

            await _context.SaveChangesAsync();

            return playlist.ToDTO();
        }

        /// <summary>
        /// Deletes a playlist.
        /// </summary>
        /// <param name="id">ID of the playlist</param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            var expectedPlaylist = await _context.Playlists.FirstOrDefaultAsync(x => x.Id == id);
            if (expectedPlaylist == null)
            {
                throw new ArgumentNullException($"Playlist with id {id} was not found.");
            }
            if (expectedPlaylist.IsDeleted)
            {
                throw new ArgumentException($"Playlist with id {id} is already deleted.");
            }

            expectedPlaylist.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Generates a playlist, saves it on the database and adds it to the User
        /// </summary>
        /// <param name="timeForTrip">Seconds of the trip</param>
        /// <param name="playlistTitle">Playlist's title</param>
        /// <param name="metalPercentagee">Percentage for Metal songs.</param>
        /// <param name="rockPercentagee">Percentage for Rock songs.</param>
        /// <param name="popPercentagee">Percentage for Pop songs.</param>
        /// <param name="topTracks">Allows using top tracks (tracks with s rank higher than 100,000).</param>
        /// <param name="sameArtist">Allows songs from the same artist.</param>
        /// <param name="user">User to add playlist to</param>
        /// <returns></returns>
        public async Task GeneratePlaylist(int timeForTrip, string playlistTitle, int metalPercentagee,
            int rockPercentagee, int popPercentagee, bool topTracks, bool sameArtist, User user)
        {
            var databasePlaylist = await Create(new PlaylistDTO { Title = playlistTitle });

            int tripTime = timeForTrip;
            int allowedOffsetMore = 5 * 60; // 5 Min +
            int allowedOffsetLess = 5 * 60; // 5 Min -


            int genresSelected = 0;


            //Проверяваме колко genres са селектирани.
            genresSelected = CheckSelectedGenres(metalPercentagee, rockPercentagee, popPercentagee);


            //Сетваме колко процента от тоталните секунди се падат за всеки genre.
            int[] offsetsMeteal = SetOffsets(metalPercentagee, allowedOffsetMore, allowedOffsetLess, genresSelected);
            int[] offsetsRock = SetOffsets(rockPercentagee, allowedOffsetMore, allowedOffsetLess, genresSelected);
            int[] offsetsPop = SetOffsets(popPercentagee, allowedOffsetMore, allowedOffsetLess, genresSelected);


            double metalPercentage = metalPercentagee / 100.0;
            double rockPercentage = rockPercentagee / 100.0;
            double popPercentage = popPercentagee / 100.0;

            bool useTopTracks = topTracks;
            bool allowSameArtist = sameArtist;

            List<Song> finalPlaylist = new List<Song>();

            if (useTopTracks && allowSameArtist)
            {
                var metalSongs = GetGenreSongs(offsetsMeteal, "metal", true);
                var metal = ExtractSongs(tripTime, offsetsMeteal[0], offsetsMeteal[1], metalPercentage, metalSongs);

                var rockSongs = GetGenreSongs(offsetsMeteal, "rock", true);
                var rock = ExtractSongs(tripTime, offsetsRock[0], offsetsRock[1], rockPercentage, rockSongs);

                var popSongs = GetGenreSongs(offsetsMeteal, "pop", true);
                var pop = ExtractSongs(tripTime, offsetsPop[0], offsetsPop[1], popPercentage, popSongs);

                AddSongsToPlaylist(finalPlaylist, metal, rock, pop);

                Shuffle(finalPlaylist);
            }
            else if (useTopTracks && allowSameArtist == false)
            {
                var metalSongs = GetGenreSongs(offsetsMeteal, "metal", true);
                List<Song> metal = ExtractSongsUniqueArtist(tripTime, offsetsMeteal[0], offsetsMeteal[1], metalPercentage, metalSongs);

                var rockSongs = GetGenreSongs(offsetsMeteal, "rock", true);
                List<Song> rock = ExtractSongsUniqueArtist(tripTime, offsetsRock[0], offsetsRock[1], rockPercentage, rockSongs);

                var popSongs = GetGenreSongs(offsetsMeteal, "pop", true);
                List<Song> pop = ExtractSongsUniqueArtist(tripTime, offsetsPop[0], offsetsPop[1], popPercentage, popSongs);

                AddSongsToPlaylist(finalPlaylist, metal, rock, pop);

                Shuffle(finalPlaylist);
            }
            else if (useTopTracks == false && allowSameArtist)
            {
                var metalSongs = GetGenreSongs(offsetsMeteal, "metal", false);
                List<Song> metal = ExtractSongs(tripTime, offsetsMeteal[0], offsetsMeteal[1], metalPercentage, metalSongs);

                var rockSongs = GetGenreSongs(offsetsMeteal, "rock", false);
                List<Song> rock = ExtractSongs(tripTime, offsetsRock[0], offsetsRock[1], rockPercentage, rockSongs);

                var popSongs = GetGenreSongs(offsetsMeteal, "pop", false);
                List<Song> pop = ExtractSongs(tripTime, offsetsPop[0], offsetsPop[1], popPercentage, popSongs);

                AddSongsToPlaylist(finalPlaylist, metal, rock, pop);

                Shuffle(finalPlaylist);
            }
            else
            {
                var metalSongs = GetGenreSongs(offsetsMeteal, "metal", false);
                List<Song> metal = ExtractSongsUniqueArtist(tripTime, offsetsMeteal[0], offsetsMeteal[1], metalPercentage, metalSongs);

                var rockSongs = GetGenreSongs(offsetsMeteal, "rock", false);
                List<Song> rock = ExtractSongsUniqueArtist(tripTime, offsetsRock[0], offsetsRock[1], rockPercentage, rockSongs);

                var popSongs = GetGenreSongs(offsetsMeteal, "pop", false);
                List<Song> pop = ExtractSongsUniqueArtist(tripTime, offsetsPop[0], offsetsPop[1], popPercentage, popSongs);

                AddSongsToPlaylist(finalPlaylist, metal, rock, pop);

                Shuffle(finalPlaylist);
            }

            int realTotalDuration = 0;
            foreach (var song in finalPlaylist)
            {
                var relation = new PlaylistsSongs() { SongId = song.Id, PlaylistId = databasePlaylist.Id };

                databasePlaylist.PlaylistsSongs.Add(relation);

                song.PlaylistsSongs.Add(relation);

                realTotalDuration += song.Duration;
            }

            databasePlaylist.Duration = realTotalDuration;
            databasePlaylist.UserId = user.Id;

            user.Playlists.Add(databasePlaylist);

            await _context.SaveChangesAsync();
        }



        /// <summary>
        /// Extracts all the possible songs of a given genre.
        /// </summary>
        /// <param name="tripTime">The total trip time</param>
        /// <param name="allowedOffsetLess">Offset for how many less seconds the mini playlist can be.</param>
        /// <param name="allowedOffsetMore">Offset for how many more seconds the mini playlist can be.</param>
        /// <param name="percentageTime">What percentage the mini playlist needs to be.</param>
        /// <param name="genreSongs">List of all the songs of a given genre.</param>
        /// <returns></returns>
        private static List<Song> ExtractSongs(int tripTime, int allowedOffsetLess, int allowedOffsetMore, double percentageTime, List<Song> genreSongs)
        {
            if (genreSongs.Count() == 0)
            {
                return genreSongs;
            }

            Shuffle(genreSongs);

            int secondsAllowed = (int)(tripTime * percentageTime);

            int currentPlaylistDuration = 0;
            List<Song> shuffledResult = new List<Song>();

            foreach (var song in genreSongs)
            {
                if (currentPlaylistDuration > secondsAllowed - allowedOffsetLess && currentPlaylistDuration < secondsAllowed + allowedOffsetMore)
                {
                    break;
                }

                if (currentPlaylistDuration > secondsAllowed + allowedOffsetMore)
                {
                    var songToRemove = shuffledResult.Last();
                    currentPlaylistDuration -= songToRemove.Duration;

                    shuffledResult.Remove(songToRemove);
                    continue;
                }

                shuffledResult.Add(song);
                currentPlaylistDuration += song.Duration;
            }

            return shuffledResult;
        }


        /// <summary>
        /// Extracts all the possible songs with unique artist of a given genre.
        /// </summary>
        /// <param name="tripTime">The total trip time.</param>
        /// <param name="allowedOffsetLess">Offset for how many less seconds the mini playlist can be.</param>
        /// <param name="allowedOffsetMore">Offset for how many more seconds the mini playlist can be.</param>
        /// <param name="percentageTime">What percentage the mini playlist needs to be.</param>
        /// <param name="genreSongs">List of all the songs of a given genre.</param>
        /// <returns></returns>
        private static List<Song> ExtractSongsUniqueArtist(int tripTime, int allowedOffsetLess, int allowedOffsetMore, double percentageTime, List<Song> genreSongs)
        {
            if (genreSongs.Count() == 0)
            {
                return genreSongs;
            }

            Shuffle(genreSongs);


            int secondsAllowed = (int)(tripTime * percentageTime);

            int currentPlaylistDuration = 0;
            Dictionary<int, Song> shuffledResult = new Dictionary<int, Song>();

            foreach (var song in genreSongs)
            {
                if (currentPlaylistDuration > secondsAllowed - allowedOffsetLess && currentPlaylistDuration < secondsAllowed + allowedOffsetMore)
                {
                    break;
                }

                if (currentPlaylistDuration > secondsAllowed + allowedOffsetMore)
                {
                    var songToRemove = shuffledResult.Values.Last();
                    currentPlaylistDuration -= songToRemove.Duration;

                    shuffledResult.Remove(songToRemove.ArtistId);
                    continue;
                }

                if (shuffledResult.TryAdd(song.ArtistId, song))
                {
                    currentPlaylistDuration += song.Duration;
                }
            }

            return shuffledResult.Values.ToList();
        }


        /// <summary>
        /// Adds all songs to a List
        /// </summary>
        /// <param name="playlistWithSongs">List where all songs will go</param>
        /// <param name="metal">List of metal songs to be added</param>
        /// <param name="rock">List of rock songs to be added</param>
        /// <param name="pop">List of pop songs to be added</param>
        private static void AddSongsToPlaylist(List<Song> playlistWithSongs, List<Song> metal, List<Song> rock, List<Song> pop)
        {
            foreach (var item in metal)
            {
                playlistWithSongs.Add(item);
            }
            foreach (var item in rock)
            {
                playlistWithSongs.Add(item);
            }
            foreach (var item in pop)
            {
                playlistWithSongs.Add(item);
            }
        }


        /// <summary>
        /// Get all songs of given genre.
        /// </summary>
        /// <param name="offsets">If 0, returns emty list.</param>
        /// <param name="genreName">Returns songs of given genre.</param>
        /// <param name="topTracks">Doesn't return songs where rank is less than 100,000.</param>
        /// <returns></returns>
        private List<Song> GetGenreSongs(int[] offsets, string genreName, bool topTracks)
        {
            if (offsets[0] != 0 && offsets[1] != 0)
            {
                if (topTracks)
                {
                    return _context.Songs.Where(x => x.Genre.Name == genreName && x.Rank >= 100000).ToList();
                }

                return _context.Songs.Where(x => x.Genre.Name == genreName).ToList();
            }

            return new List<Song>();
        }


        /// <summary>
        /// Sets how many seconds offset a genre has
        /// </summary>
        /// <param name="percentage">If 0, returns {0, 0}</param>
        /// <param name="allowedOffsetMore"></param>
        /// <param name="allowedOffsetLess"></param>
        /// <param name="genre"></param>
        /// <returns></returns>
        private static int[] SetOffsets(int percentage, int allowedOffsetMore, int allowedOffsetLess, int genre)
        {
            int[] offcets = { 0, 0 };
            if (percentage != 0)
            {
                offcets[0] = allowedOffsetLess / genre;
                offcets[1] = allowedOffsetMore / genre;
            }

            return offcets;
        }


        /// <summary>
        /// Returns how many genres are selected
        /// </summary>
        /// <param name="metalPercentagee"></param>
        /// <param name="rockPercentagee"></param>
        /// <param name="popPercentagee"></param>
        /// <returns></returns>
        private static int CheckSelectedGenres(int metalPercentagee, int rockPercentagee, int popPercentagee)
        {
            int genresSelected = 0;

            if (metalPercentagee != 0)
            {
                genresSelected++;
            }
            if (rockPercentagee != 0)
            {
                genresSelected++;
            }
            if (popPercentagee != 0)
            {
                genresSelected++;
            }

            return genresSelected;
        }


        //Това не трябва да е тука, ама за сега ще е :D
        private static readonly Random rng = new Random();

        private static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        //----------------------------------------------
    }

}
