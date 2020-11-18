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

        public async Task<IEnumerable<PlaylistDTO>> GetAllPlaylists()
        {
            return await _context.Playlists
                                 .Include(x => x.PlaylistsSongs)
                                 .ThenInclude(x => x.Song)
                                 .Where(x => x.IsDeleted == false)
                                 .Select(x => x.ToDTO())
                                 .ToListAsync();
        }

        public async Task<IEnumerable<PlaylistDTO>> GetPlaylistsByUser(string id)
        {
            return await _context.Playlists
                                 .Include(x => x.PlaylistsSongs)
                                 .ThenInclude(x => x.Song)
                                 .Where(x => x.UserId == id && x.IsDeleted == false)
                                 .Select(x => x.ToDTO())
                                 .ToListAsync();
        }

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

        public async Task<PlaylistDTO> Update(int id, PlaylistDTO playlistDTO)
        {
            var playlist = await _context.Playlists.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (playlist == null)
            {
                throw new ArgumentNullException($"Playlist with id {id} was not found.");
            }

            playlist.Title = playlistDTO.Title;
            //playlist.Description = playlistDTO.Description;
            playlist.Duration = playlistDTO.Duration;
            //playlist.Fans = playlistDTO.Fans;
            //playlist.Link = playlistDTO.Link;
            //playlist.Share = playlistDTO.Share;
            playlist.Picture = playlistDTO.Picture;
            //playlist.Tracklist = playlistDTO.Tracklist;
            //playlist.Creation_date = playlistDTO.Creation_date;
            //playlist.Type = playlistDTO.Type;

            await _context.SaveChangesAsync();

            return playlist.ToDTO();
        }

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

        public async Task GeneratePlaylist(int timeForTrip, string playlistName, int metalPercentagee,
            int rockPercentagee, int popPercentagee, bool topTracks, bool sameArtist, User user)
        {
            var playlistAdded = await Create(new PlaylistDTO { Title = playlistName });

            int tripTime = timeForTrip;
            int allowedOffsetMore = 5 * 60; // 5 Min +
            int allowedOffsetLess = 5 * 60; // 5 Min -


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


            int[] offsetsMeteal = { 0, 0 };
            if (metalPercentagee != 0)
            {
                offsetsMeteal[0] = allowedOffsetLess / genresSelected;
                offsetsMeteal[1] = allowedOffsetMore / genresSelected;
            }

            int[] offsetsRock = { 0, 0 };
            if (rockPercentagee != 0)
            {
                offsetsRock[0] = allowedOffsetLess / genresSelected;
                offsetsRock[1] = allowedOffsetMore / genresSelected;
            }

            int[] offsetsPop = { 0, 0 };
            if (popPercentagee != 0)
            {
                offsetsPop[0] = allowedOffsetLess / genresSelected;
                offsetsPop[1] = allowedOffsetMore / genresSelected;
            }

            //TODO: 
            double metalPercentage = metalPercentagee / 100.0;
            double rockPercentage = rockPercentagee / 100.0;
            double popPercentage = popPercentagee / 100.0;

            bool useTopTracks = topTracks;
            bool allowSameArtist = sameArtist;

            List<Song> playlistWithSongs = new List<Song>();

            if (useTopTracks == true && allowSameArtist == true)
            {
                var metalSongs = new List<Song>();
                if (offsetsMeteal[0] != 0 && offsetsMeteal[1] != 0)
                {
                    metalSongs = _context.Songs.Where(x => x.Genre.Name == "metal" && x.Rank >= 100000).ToList();
                }
                List<Song> metal = ExtractSongs(tripTime, offsetsMeteal[0], offsetsMeteal[1], metalPercentage, metalSongs);

                var rockSongs = new List<Song>();
                if (offsetsRock[0] != 0 && offsetsRock[1] != 0)
                {
                    rockSongs = _context.Songs.Where(x => x.Genre.Name == "rock" && x.Rank >= 100000).ToList();
                }
                List<Song> rock = ExtractSongs(tripTime, offsetsRock[0], offsetsRock[1], rockPercentage, rockSongs);

                var popSongs = new List<Song>();
                if (offsetsPop[0] != 0 && offsetsPop[1] != 0)
                {
                    popSongs = _context.Songs.Where(x => x.Genre.Name == "pop" && x.Rank >= 100000).ToList();
                }
                List<Song> pop = ExtractSongs(tripTime, offsetsPop[0], offsetsPop[1], popPercentage, popSongs);


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
            else if (useTopTracks == true && allowSameArtist == false)
            {
                var metalSongs = new List<Song>();
                if (offsetsMeteal[0] != 0 && offsetsMeteal[1] != 0)
                {
                    metalSongs = _context.Songs.Where(x => x.Genre.Name == "metal" && x.Rank >= 100000).ToList();
                }
                List<Song>metal = ExtractSongsUniqueArtist(tripTime, offsetsMeteal[0], offsetsMeteal[1], metalPercentage, metalSongs);


                var rockSongs = new List<Song>();
                if (offsetsRock[0] != 0 && offsetsRock[1] != 0)
                {
                    rockSongs = _context.Songs.Where(x => x.Genre.Name == "rock" && x.Rank >= 100000).ToList();
                }
                List<Song> rock = ExtractSongsUniqueArtist(tripTime, offsetsRock[0], offsetsRock[1], rockPercentage, rockSongs);


                var popSongs = new List<Song>();
                if (offsetsPop[0] != 0 && offsetsPop[1] != 0)
                {
                    popSongs = _context.Songs.Where(x => x.Genre.Name == "pop" && x.Rank >= 100000).ToList();
                }
                List<Song> pop = ExtractSongsUniqueArtist(tripTime, offsetsPop[0], offsetsPop[1], popPercentage, popSongs);



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
            else if (useTopTracks == false && allowSameArtist == true)
            {
                var metalSongs = new List<Song>();
                if (offsetsMeteal[0] != 0 && offsetsMeteal[1] != 0)
                {
                    metalSongs = _context.Songs.Where(x => x.Genre.Name == "metal").ToList();
                }
                List<Song> metal = ExtractSongs(tripTime, offsetsMeteal[0], offsetsMeteal[1], metalPercentage, metalSongs);

                var rockSongs = new List<Song>();
                if (offsetsRock[0] != 0 && offsetsRock[1] != 0)
                {
                    rockSongs = _context.Songs.Where(x => x.Genre.Name == "rock").ToList();
                }
                List<Song> rock = ExtractSongs(tripTime, offsetsRock[0], offsetsRock[1], rockPercentage, rockSongs);

                var popSongs = new List<Song>();
                if (offsetsPop[0] != 0 && offsetsPop[1] != 0)
                {
                    popSongs = _context.Songs.Where(x => x.Genre.Name == "pop").ToList();
                }
                List<Song> pop = ExtractSongs(tripTime, offsetsPop[0], offsetsPop[1], popPercentage, popSongs);


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
            else
            {
                var metalSongs = new List<Song>();
                if (offsetsMeteal[0] != 0 && offsetsMeteal[1] != 0)
                {
                    metalSongs = _context.Songs.Where(x => x.Genre.Name == "metal").ToList();
                }
                List<Song> metal = ExtractSongsUniqueArtist(tripTime, offsetsMeteal[0], offsetsMeteal[1], metalPercentage, metalSongs);


                var rockSongs = new List<Song>();
                if (offsetsRock[0] != 0 && offsetsRock[1] != 0)
                {
                    rockSongs = _context.Songs.Where(x => x.Genre.Name == "rock").ToList();
                }
                List<Song> rock = ExtractSongsUniqueArtist(tripTime, offsetsRock[0], offsetsRock[1], rockPercentage, rockSongs);


                var popSongs = new List<Song>();
                if (offsetsPop[0] != 0 && offsetsPop[1] != 0)
                {
                    popSongs = _context.Songs.Where(x => x.Genre.Name == "pop").ToList();
                }
                List<Song> pop = ExtractSongsUniqueArtist(tripTime, offsetsPop[0], offsetsPop[1], popPercentage, popSongs);


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



            int realTotalDuration = 0;
            foreach (var song in playlistWithSongs)
            {
                var relation = new PlaylistsSongs() { SongId = song.Id, PlaylistId = playlistAdded.Id };

                playlistAdded.PlaylistsSongs.Add(relation);
                song.PlaylistsSongs.Add(relation);

                realTotalDuration += song.Duration;
            }

            playlistAdded.Duration = realTotalDuration;
            playlistAdded.UserId = user.Id;
            user.Playlists.Add(playlistAdded);

            await _context.SaveChangesAsync();
        }



        private static List<Song> ExtractSongs(int tripTime, int allowedOffsetLess, int allowedOffsetMore, double percentage, List<Song> result)
        {
            if (result.Count() == 0)
            {
                return result;
            }

            Shuffle(result);


            int secondsAllowed = (int)(tripTime * percentage);

            int count = 0;
            List<Song> shuffledResult = new List<Song>();

            foreach (var song in result)
            {
                if (count > secondsAllowed - allowedOffsetLess && count < secondsAllowed + allowedOffsetMore)
                {
                    break;
                }

                if (count > secondsAllowed + allowedOffsetMore)
                {
                    var toRemove = shuffledResult.Last();
                    count -= toRemove.Duration;

                    shuffledResult.Remove(toRemove);
                    continue;
                }

                shuffledResult.Add(song);
                count += song.Duration;

            }

            return shuffledResult;
        }

        private static List<Song> ExtractSongsUniqueArtist(int tripTime, int allowedOffsetLess, int allowedOffsetMore, double percentage, List<Song> result)
        {
            if (result.Count() == 0)
            {
                return result;
            }

            Shuffle(result);


            int secondsAllowed = (int)(tripTime * percentage);

            int count = 0;
            Dictionary<int, Song> shuffledResult = new Dictionary<int, Song>();

            foreach (var song in result)
            {
                if (count > secondsAllowed - allowedOffsetLess && count < secondsAllowed + allowedOffsetMore)
                {
                    break;
                }

                if (count > secondsAllowed + allowedOffsetMore)
                {
                    var toRemove = shuffledResult.Values.Last();
                    count -= toRemove.Duration;

                    shuffledResult.Remove(toRemove.ArtistId);
                    continue;
                }

                if (shuffledResult.TryAdd(song.ArtistId, song))
                {
                    count += song.Duration;
                }
            }

            return shuffledResult.Values.ToList();
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
