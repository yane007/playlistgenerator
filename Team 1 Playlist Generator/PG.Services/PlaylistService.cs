﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PG.Data.Context;
using PG.Models;
using PG.Services.Contract;
using PG.Services.DTOs;
using PG.Services.Exceptions;
using PG.Services.Mappers;
using PG.Services.MappingModelsAPI.Pixabay;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace PG.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly PGDbContext _context;
        private readonly IPixabayService pixabayService;

        public PlaylistService(PGDbContext context, IPixabayService pixabayService)
        {
            _context = context;
            this.pixabayService = pixabayService;
        }

        public async Task<Playlist> Create(PlaylistDTO playlistDTO)
        {
            if (playlistDTO == null)
            {
                throw new NotFoundException("Null Playlist");
            }
            if (playlistDTO.Title.Length > 50)
            {
                throw new OutOfRangeException("Playlist's title needs to be shorter than 50 characters.");
            }

            Playlist playlistToAdd = playlistDTO.ToEntity();
            var playlist = _context.Playlists.Add(playlistToAdd);

            Log.Logger.Information($"Playlist with title '{playlist.Entity.Title}' has been created.");

            await _context.SaveChangesAsync();

            return playlist.Entity;
        }

        public async Task<IEnumerable<PlaylistDTO>> GetAllPlaylists()
        {
            var playlists = await _context.Playlists
                                 .Include(x => x.PlaylistsSongs)
                                 .ThenInclude(x => x.Song)
                                 .Include(x => x.User)
                                 .Where(x => !x.IsDeleted)
                                 .Select(x => x.ToDTO())
                                 .ToListAsync();

            return playlists.OrderByDescending(x => x.Rank).ToList();
        }

        public async Task<IEnumerable<PlaylistDTO>> GetPlaylistsByUser(string userId)
        {
            var playlists = await _context.Playlists
                                 .Include(x => x.PlaylistsSongs)
                                 .ThenInclude(x => x.Song)
                                 .Include(x => x.User)
                                 .Where(x => x.UserId == userId && !x.IsDeleted)
                                 .Select(x => x.ToDTO())
                                 .ToListAsync();

            return playlists.OrderByDescending(x => x.Rank).ToList();

        }

        public async Task<PlaylistDTO> GetPlaylistById(int id)
        {
            var playlist = await _context.Playlists
                                 .Include(x => x.PlaylistsSongs)
                                 .ThenInclude(x => x.Song)
                                 .Include(x => x.User)
                                 .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            if (playlist == null)
            {
                throw new NotFoundException($"Playlist with id {id} was not found.");
            }

            return playlist.ToDTO();
        }

        public async Task<PlaylistDTO> Update(int id, PlaylistDTO playlistDTO)
        {
            var playlist = await _context.Playlists.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (playlist == null)
            {
                throw new NotFoundException($"Playlist with id {id} was not found.");
            }

            playlist.Title = playlistDTO.Title;
            playlist.PixabayImage = playlistDTO.PixabayImage;

            await _context.SaveChangesAsync();

            return playlist.ToDTO();
        }

        public async Task UpdatePublicAccess(int id)
        {
            var playlist = await _context.Playlists.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (playlist == null)
            {
                throw new NotFoundException($"Playlist with id {id} was not found.");
            }

            if (playlist.IsPublic)
            {
                playlist.IsPublic = false;
            }
            else
            {
                playlist.IsPublic = true;
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                throw new OutOfRangeException("Id can't be 0 or negative.");
            }

            var expectedPlaylist = await _context.Playlists.FirstOrDefaultAsync(x => x.Id == id);
            if (expectedPlaylist == null)
            {
                throw new NotFoundException($"Playlist with id {id} was not found.");
            }
            if (expectedPlaylist.IsDeleted)
            {
                throw new ArgumentException($"Playlist with id {id} is already deleted.");
            }

            expectedPlaylist.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetMinPlaylistDuration()
        {
            var playlists = await GetAllPlaylists();

            if (playlists.Count() == 0)
            {
                return 0;
            }

            var shortestPlaylist = playlists.OrderBy(x => x.Duration).First();

            return shortestPlaylist.Duration;
        }

        public async Task<int> GetMaxPlaylistDuration()
        {
            var playlists = await GetAllPlaylists();

            if (playlists.Count() == 0)
            {
                return 0;
            }

            var shortestPlaylist = playlists.OrderByDescending(x => x.Duration).First();

            return shortestPlaylist.Duration;
        }

        public async Task<IEnumerable<PlaylistDTO>> GetAllPlaylistsWithSettings(string nameLike, string genre, string duration)
        {
            int intDuration = 0;
            if (duration == "" || duration == null)
            {
                intDuration = int.MaxValue;
            }
            else
            {
                intDuration = int.Parse(duration);
            }

            if (nameLike == null)
            {
                nameLike = "";
            }

            if (genre == "" || genre == null)
            {
                var playlists = await _context.Playlists
                     .Include(x => x.PlaylistsSongs)
                     .ThenInclude(x => x.Song)
                     .Include(x => x.User)
                     .Where(x => !x.IsDeleted
                             && x.Title.Contains(nameLike)
                                && x.Duration <= intDuration
                                )
                     .Select(x => x.ToDTO())
                     .ToListAsync();

                return playlists.OrderByDescending(x => x.Rank).ToList();
            }
            else
            {
                var playlists = await _context.Playlists
                     .Include(x => x.PlaylistsSongs)
                     .ThenInclude(x => x.Song)
                     .Include(x => x.User)
                     .Include(x => x.PlaylistsGenres)
                     .ThenInclude(x => x.Genre)
                     .Where(x => !x.IsDeleted
                             && x.Title.Contains(nameLike)

                                && x.PlaylistsGenres
                                   .Select(x => x.Genre.Name)
                                   .Contains(genre)

                                && x.Duration <= intDuration
                                )
                     .Select(x => x.ToDTO())
                     .ToListAsync();

                return playlists.OrderByDescending(x => x.Rank).ToList();
            }

        }

        public async Task GeneratePlaylist(int tripTime, string playlistTitle, int metalPercentagee,
            int rockPercentagee, int popPercentagee, int chalgaPercentage, bool useTopTracks, bool allowSameArtist, User user)
        {
            var databasePlaylist = await Create(new PlaylistDTO { Title = playlistTitle });

            int allowedOffsetMore = 5 * 60; // 5 Min +
            int allowedOffsetLess = 5 * 60; // 5 Min -

            //TODO: да се приемат колкото и да са 
            var listGenres = new List<Tuple<string, int>>
            {
                new Tuple<string, int> ("metal", metalPercentagee),
                new Tuple<string, int> ("rock", rockPercentagee),
                new Tuple<string, int> ("pop", popPercentagee),
                new Tuple<string, int> ("chalga", chalgaPercentage),
            };

            listGenres = FillGenresPercentageToMax(listGenres);

            int countGenresSelected = await AddSelectedGenresToPlaylist(listGenres, _context, databasePlaylist);

            List<Tuple<string, int[], double>> namesOffsetsAndPercentages = SetOffsets(
                listGenres,
                allowedOffsetMore,
                allowedOffsetLess,
                countGenresSelected);


            List<Song> finalPlaylist = new List<Song>();


            if (useTopTracks && allowSameArtist)
            {
                GenerateByTopTracksSameArtist(tripTime, namesOffsetsAndPercentages, finalPlaylist);
            }
            else if (useTopTracks && !allowSameArtist)
            {
                GenerateByTopTracks(tripTime, namesOffsetsAndPercentages, finalPlaylist);
            }
            else if (!useTopTracks && allowSameArtist)
            {
                GenerateBySameArtist(tripTime, namesOffsetsAndPercentages, finalPlaylist);
            }
            else
            {
                GenerateWithoutBoth(tripTime, namesOffsetsAndPercentages, finalPlaylist);
            }

            AddDataToPlaylist(user, databasePlaylist, finalPlaylist);

            user.Playlists.Add(databasePlaylist);

            await AddPixabayImageToPlaylist(databasePlaylist);

            await _context.SaveChangesAsync();
        }

        private static void AddDataToPlaylist(User user, Playlist databasePlaylist, List<Song> finalPlaylist)
        {
            int realTotalDuration = 0;
            int totalRank = 0;
            int totalSongsCount = 0;

            foreach (var song in finalPlaylist)
            {
                var relation = new PlaylistsSongs() { SongId = song.Id, PlaylistId = databasePlaylist.Id };

                databasePlaylist.PlaylistsSongs.Add(relation);

                song.PlaylistsSongs.Add(relation);

                realTotalDuration += song.Duration;
                totalRank += song.Rank;
                totalSongsCount++;
            }

            databasePlaylist.Duration = realTotalDuration;
            databasePlaylist.UserId = user.Id;

            if (totalSongsCount == 0)
            {
                databasePlaylist.Rank = 0;
            }
            else
            {
                databasePlaylist.Rank = totalRank / totalSongsCount;
            }
        }

        private List<Tuple<string, int>> FillGenresPercentageToMax(List<Tuple<string, int>> listGenres)
        {
            float total = listGenres.Select(x => x.Item2).Sum();

            for (int i = 0; i < listGenres.Count; i++)
            {
                listGenres[i] = new Tuple<string, int> (listGenres[i].Item1, (int)Math.Round( listGenres[i].Item2 / total * 100));
            }

            return listGenres;

            //listGenres = listGenres.OrderByDescending(x => x.Item2).ToList();

            //int genresTotalPercentage = CheckGenresTotalPercentage(listGenres);

            //if (genresTotalPercentage == 100)
            //{
            //    return listGenres;
            //}

            //else if (listGenres.Any() && genresTotalPercentage < 100)
            //{
            //    int newGenrePercentage = 100 - genresTotalPercentage + listGenres[0].Item2;
            //    listGenres[0] = new Tuple<string, int>(listGenres[0].Item1, newGenrePercentage);
            //}

            //else
            //{
            //    while (genresTotalPercentage > 100)
            //    {
            //        listGenres = TurnAllPercentagesDownByOne(listGenres);
            //        genresTotalPercentage = CheckGenresTotalPercentage(listGenres);
            //    }

            //    listGenres = FillGenresPercentageToMax(listGenres);
            //}

            //return listGenres;
        }

        private List<Tuple<string, int>> TurnAllPercentagesDownByOne(List<Tuple<string, int>> listGenres)
        {
            for (int i = 0; i < listGenres.Count; i++)
            {
                int newPercentage = listGenres[i].Item2 - 1;

                if (newPercentage < 0)
                {
                    newPercentage = 0;
                }

                listGenres[i] = new Tuple<string, int>(listGenres[i].Item1, newPercentage);
            }

            return listGenres;
        }

        private int CheckGenresTotalPercentage(List<Tuple<string, int>> listGenres)
        {
            int totalPercentage = 0;
            foreach (var item in listGenres)
            {
                totalPercentage += item.Item2;
            }

            return totalPercentage;
        }

        private void GenerateWithoutBoth(int tripTime, List<Tuple<string, int[], double>> namesOffsetsAndPercentages, List<Song> finalPlaylist)
        {
            foreach (var item in namesOffsetsAndPercentages)
            {
                var genreSongs = GetGenreSongs(item.Item2, item.Item1, false);
                var extractedSongs = ExtractSongsUniqueArtist(tripTime, item.Item2[0], item.Item2[1], item.Item3, genreSongs);

                foreach (var song in extractedSongs)
                {
                    finalPlaylist.Add(song);
                }
            }

            Shuffle(finalPlaylist);
        }

        private void GenerateBySameArtist(int tripTime, List<Tuple<string, int[], double>> namesOffsetsAndPercentages, List<Song> finalPlaylist)
        {
            foreach (var item in namesOffsetsAndPercentages)
            {
                var genreSongs = GetGenreSongs(item.Item2, item.Item1, false);
                var extractedSongs = ExtractSongs(tripTime, item.Item2[0], item.Item2[1], item.Item3, genreSongs);

                foreach (var song in extractedSongs)
                {
                    finalPlaylist.Add(song);
                }
            }

            Shuffle(finalPlaylist);
        }

        private void GenerateByTopTracks(int tripTime, List<Tuple<string, int[], double>> namesOffsetsAndPercentages, List<Song> finalPlaylist)
        {
            foreach (var item in namesOffsetsAndPercentages)
            {
                var genreSongs = GetGenreSongs(item.Item2, item.Item1, true);
                var extractedSongs = ExtractSongsUniqueArtist(tripTime, item.Item2[0], item.Item2[1], item.Item3, genreSongs);

                foreach (var song in extractedSongs)
                {
                    finalPlaylist.Add(song);
                }
            }

            Shuffle(finalPlaylist);
        }

        private void GenerateByTopTracksSameArtist(int tripTime, List<Tuple<string, int[], double>> namesOffsetsAndPercentages, List<Song> finalPlaylist)
        {
            foreach (var item in namesOffsetsAndPercentages)
            {
                var genreSongs = GetGenreSongs(item.Item2, item.Item1, true);
                var extractedSongs = ExtractSongs(tripTime, item.Item2[0], item.Item2[1], item.Item3, genreSongs);

                foreach (var song in extractedSongs)
                {
                    finalPlaylist.Add(song);
                }
            }

            Shuffle(finalPlaylist);
        }

        private async Task AddPixabayImageToPlaylist(Playlist databasePlaylist)
        {
            string pixabayImage = await pixabayService.GetPixabayImage(databasePlaylist.Id);

            databasePlaylist.PixabayImage = pixabayImage;
        }

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

        private List<Song> GetGenreSongs(int[] offsets, string genreName, bool topTracks)
        {
            if (offsets[0] != 0 && offsets[1] != 0)
            {
                if (topTracks)
                {
                    return _context.Songs.Where(x => x.Genre.Name == genreName && x.Rank >= 90000).ToList();
                }

                return _context.Songs.Where(x => x.Genre.Name == genreName).ToList();
            }

            return new List<Song>();
        }

        private List<Tuple<string, int[], double>> SetOffsets(List<Tuple<string, int>> listGenres,
            int allowedOffsetMore, int allowedOffsetLess, int selectedGenres)
        {
            var toReturn = new List<Tuple<string, int[], double>>();

            foreach (var item in listGenres)
            {
                int[] offcets = { 0, 0 };
                if (item.Item2 != 0)
                {
                    offcets[0] = allowedOffsetLess / selectedGenres;
                    offcets[1] = allowedOffsetMore / selectedGenres;
                }
                toReturn.Add(new Tuple<string, int[], double>(item.Item1, offcets, item.Item2 / 100.0));
            }

            return toReturn;
        }

        private static async Task<int> AddSelectedGenresToPlaylist(List<Tuple<string, int>> data, PGDbContext _context, Playlist databasePlaylist)
        {
            int genresSelected = 0;

            foreach (var item in data)
            {
                if (item.Item2 != 0)
                {
                    //TODO: For improvement
                    var dbGenre = await _context.Genres.Include(x => x.PlaylistsGenres).FirstOrDefaultAsync(x => x.Name == item.Item1);
                    var playlistGenresLink = new PlaylistsGenres { PlaylistId = databasePlaylist.Id, GenreId = dbGenre.Id };

                    dbGenre.PlaylistsGenres.Add(playlistGenresLink);
                    databasePlaylist.PlaylistsGenres.Add(playlistGenresLink);

                    genresSelected++;
                }
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
    }
}
