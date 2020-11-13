using Microsoft.EntityFrameworkCore;
using PG.Data.Context;
using PG.Models;
using PG.Services.Contract;
using PG.Services.DTOs;
using PG.Services.Mappers;
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

        public async Task<IEnumerable<PlaylistDTO>> GetPlaylistsByUser(int id)
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
            playlist.Description = playlistDTO.Description;
            playlist.Duration = playlistDTO.Duration;
            playlist.Fans = playlistDTO.Fans;
            playlist.Link = playlistDTO.Link;
            playlist.Share = playlistDTO.Share;
            playlist.Picture = playlistDTO.Picture;
            playlist.Tracklist = playlistDTO.Tracklist;
            playlist.Creation_date = playlistDTO.Creation_date;
            playlist.Type = playlistDTO.Type;

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

        public async Task GeneratePlaylist(PlaylistDTO playlist)
        {
            var playlistAdded = await Create(playlist);

            int tripTime = 10000;
            int allowedOffsetMore = 5 * 60; // 5 Min +
            int allowedOffsetLess = 5 * 60; // 5 Min -

            //TODO: 
            int[] offsetsMeteal = { allowedOffsetLess / 2, allowedOffsetMore / 2 };
            int[] offsetsRock = { 0, 0 };
            int[] offsetsPop = { allowedOffsetLess / 2, allowedOffsetMore / 2 };


            //TODO: 
            double metalPercentage = 20 / 100.0;
            double rockPercentage = 80 / 100.0;
            double popPercentage = 0 / 100.0;

            bool useTopTracks = true;
            bool allowSameArtist = true;

            List<Song> playlistWithSongs = new List<Song>();

            if (useTopTracks == true && allowSameArtist == true)
            {

                //var metalSongs = _context.Songs.Where(x => x.Genre.Name == "metal" && x.Rank == 100000).ToList();
                //(List<Song> songs, int[] offsetsNew) metal = ExtractSongs(tripTime, offsetsMeteal[0], offsetsMeteal[1], metalPercentage, metalSongs);


                //var rockSongs = _context.Songs.Where(x => x.Genre.Name == "rock" && x.Rank == 100000).ToList();
                //offsetsRock[0] += metal.offsetsNew[0];
                //offsetsRock[1] += metal.offsetsNew[1];
                //(List<Song> songs, int[] offsetsNew) rock = ExtractSongs(tripTime, offsetsRock[0], offsetsRock[1], rockPercentage, rockSongs);


                //var popSongs = _context.Songs.Where(x => x.Genre.Name == "pop" && x.Rank == 100000).ToList();
                //offsetsPop[0] += rock.offsetsNew[0];
                //offsetsPop[1] += rock.offsetsNew[1];
                //(List<Song> songs, int[] offsetsNew) pop = ExtractSongs(tripTime, offsetsPop[0], offsetsPop[1], popPercentage, popSongs);
                var popSongs = _context.Songs.Where(x => x.Genre.Name == "pop" && x.Rank == 100000).ToList();
                (List<Song> songs, int[] offsetsNew) pop = ExtractSongs(tripTime, offsetsPop[0], offsetsPop[1], popPercentage, popSongs);


                var metalSongs = _context.Songs.Where(x => x.Genre.Name == "metal" && x.Rank == 100000).ToList();
                offsetsPop[0] += pop.offsetsNew[0];
                offsetsPop[1] += pop.offsetsNew[1];
                (List<Song> songs, int[] offsetsNew) metal = ExtractSongs(tripTime, offsetsMeteal[0], offsetsMeteal[1], metalPercentage, metalSongs);

                var rockSongs = _context.Songs.Where(x => x.Genre.Name == "rock" && x.Rank == 100000).ToList();
                offsetsRock[0] += pop.offsetsNew[0];
                offsetsRock[1] += pop.offsetsNew[1];
                (List<Song> songs, int[] offsetsNew) rock = ExtractSongs(tripTime, offsetsRock[0], offsetsRock[1], rockPercentage, rockSongs);




                foreach (var item in metal.songs)
                {
                    playlistWithSongs.Add(item);
                }
                foreach (var item in rock.songs)
                {
                    playlistWithSongs.Add(item);
                }
                foreach (var item in pop.songs)
                {
                    playlistWithSongs.Add(item);
                }
            }
            else if (useTopTracks == true && allowSameArtist == false)
            {
                var metalSongs = _context.Songs.Where(x => x.Genre.Name == "metal" && x.Rank == 100000).ToList();
                (List<Song> songs, int[] offsetsNew) metal = ExtractSongsUniqueArtist(tripTime, allowedOffsetLess, allowedOffsetMore, metalPercentage, metalSongs);

                var rockSongs = _context.Songs.Where(x => x.Genre.Name == "rock" && x.Rank == 100000).ToList();
                (List<Song> songs, int[] offsetsNew) rock = ExtractSongsUniqueArtist(tripTime, allowedOffsetLess, allowedOffsetMore, rockPercentage, rockSongs);

                var popSongs = _context.Songs.Where(x => x.Genre.Name == "pop" && x.Rank == 100000).ToList();
                (List<Song> songs, int[] offsetsNew) pop = ExtractSongsUniqueArtist(tripTime, allowedOffsetLess, allowedOffsetMore, popPercentage, popSongs);

                foreach (var item in metal.songs)
                {
                    playlistWithSongs.Add(item);
                }
                foreach (var item in rock.songs)
                {
                    playlistWithSongs.Add(item);
                }
                foreach (var item in pop.songs)
                {
                    playlistWithSongs.Add(item);
                }
            }
            else if (useTopTracks == false && allowSameArtist == true)
            {
                var metalSongs = _context.Songs.Where(x => x.Genre.Name == "metal").ToList();
                (List<Song> songs, int[] offsetsNew) metal = ExtractSongs(tripTime, allowedOffsetLess, allowedOffsetMore, metalPercentage, metalSongs);

                var rockSongs = _context.Songs.Where(x => x.Genre.Name == "rock").ToList();
                (List<Song> songs, int[] offsetsNew) rock = ExtractSongs(tripTime, allowedOffsetLess, allowedOffsetMore, rockPercentage, rockSongs);

                var popSongs = _context.Songs.Where(x => x.Genre.Name == "pop").ToList();
                (List<Song> songs, int[] offsetsNew) pop = ExtractSongs(tripTime, allowedOffsetLess, allowedOffsetMore, popPercentage, popSongs);

                foreach (var item in metal.songs)
                {
                    playlistWithSongs.Add(item);
                }
                foreach (var item in rock.songs)
                {
                    playlistWithSongs.Add(item);
                }
                foreach (var item in pop.songs)
                {
                    playlistWithSongs.Add(item);
                }
            }
            else
            {
                var metalSongs = _context.Songs.Where(x => x.Genre.Name == "metal").ToList();
                (List<Song> songs, int[] offsetsNew) metal = ExtractSongsUniqueArtist(tripTime, allowedOffsetLess, allowedOffsetMore, metalPercentage, metalSongs);

                var rockSongs = _context.Songs.Where(x => x.Genre.Name == "rock").ToList();
                (List<Song> songs, int[] offsetsNew) rock = ExtractSongsUniqueArtist(tripTime, allowedOffsetLess, allowedOffsetMore, rockPercentage, rockSongs);

                var popSongs = _context.Songs.Where(x => x.Genre.Name == "pop").ToList();
                (List<Song> songs, int[] offsetsNew) pop = ExtractSongsUniqueArtist(tripTime, allowedOffsetLess, allowedOffsetMore, popPercentage, popSongs);

                foreach (var item in metal.songs)
                {
                    playlistWithSongs.Add(item);
                }
                foreach (var item in rock.songs)
                {
                    playlistWithSongs.Add(item);
                }
                foreach (var item in pop.songs)
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

            await _context.SaveChangesAsync();
        }

        private static (List<Song>, int[]) ExtractSongs(int tripTime, int allowedOffsetLess, int allowedOffsetMore, double percentage, List<Song> result)
        {
            if (result.Count() == 0)
            {
                int[] res = { allowedOffsetLess, allowedOffsetMore };

                return (result, res);
            }
            Shuffle(result);


            int secondsAllowed = (int)(tripTime * percentage);

            int count = 0;
            var shuffledResult = result.TakeWhile(x => (count = count + x.Duration) <= secondsAllowed).ToList();


            int songsDuration = 0;
            foreach (Song song in shuffledResult)
            {
                songsDuration += song.Duration;
            }

            //Ако времето на песните е по-малко от минималното добавяме песен която влиза в диапазона 
            if (songsDuration < secondsAllowed - allowedOffsetLess)
            {
                int songsCount = shuffledResult.Count();

                var songsToTryAdd = result.Skip(songsCount).Take(1);
                if (songsToTryAdd == null || songsToTryAdd.Count() == 0)
                {
                    int[] offsets = { allowedOffsetLess -= secondsAllowed - songsDuration, allowedOffsetMore };

                    return (shuffledResult, offsets);
                }
                while (true)
                {
                    int newSongDuration = songsToTryAdd.ToList()[0].Duration;

                    //ако надвиши максимума изцяло, взимаме друга песен
                    if (newSongDuration > allowedOffsetLess + allowedOffsetMore)
                    {
                        songsCount++;
                        var songsToTryAdd2 = result.Skip(songsCount);
                        if (songsToTryAdd2 == null || songsToTryAdd2.Count() == 0)
                        {
                            //няма песен която да пасва

                            shuffledResult.RemoveAt(shuffledResult.Count() - 1);
                            songsCount = shuffledResult.Count();
                        }
                        else
                        {
                            songsToTryAdd = songsToTryAdd2;
                        }
                        continue;
                    }
                    else
                    {
                        //setvame pesenta, vliza v diapazona
                        shuffledResult.Add(songsToTryAdd.First());

                        int newSongDuration2 = songsToTryAdd.First().Duration;

                        if (newSongDuration2 >= allowedOffsetLess)
                        {
                            allowedOffsetMore -= (newSongDuration2 - allowedOffsetLess);
                            allowedOffsetLess = 0;
                        }
                        else
                        {
                            allowedOffsetLess -= songsToTryAdd.First().Duration;
                        }
                        int[] offsets = { allowedOffsetLess, allowedOffsetMore };


                        return (shuffledResult, offsets);

                    }
                }

            }
            else
            {
                int[] arr = { allowedOffsetLess += secondsAllowed-songsDuration, allowedOffsetMore };

                return (shuffledResult, arr);
            }



        }

        private static (List<Song>, int[]) ExtractSongsUniqueArtist(int tripTime, int allowedOffsetLess,
            int allowedOffsetMore, double popPercentage, List<Song> incomingList)
        {
            if (incomingList.Count() == 0)
            {
                int[] res = { allowedOffsetLess, allowedOffsetMore };

                return (incomingList, res);
            }
            Shuffle(incomingList);

            //Процентите не трябва да са по-малки от 0.X
            int secondsAllowed = (int)(tripTime * popPercentage);

            int count = 0;
            var result = incomingList.TakeWhile(x => (count = count + x.Duration) <= secondsAllowed).ToDictionary(x => x.Artist);


            int songsDuration = 0;
            foreach (Song song in result.Values)
            {
                songsDuration += song.Duration;
            }

            //Ако времето на песните е по-малко от минималното добавяме песен която влиза в диапазона 
            if (songsDuration < secondsAllowed - allowedOffsetLess)
            {
                int songsCount = result.Count();

                var songsToTryAdd = incomingList.Skip(songsCount).Take(1);
                if (songsToTryAdd == null || songsToTryAdd.Count() == 0)
                {
                    int[] offsets = { allowedOffsetLess -= secondsAllowed - songsDuration, allowedOffsetMore };

                    return (result.Values.ToList(), offsets);
                }
                while (true)
                {
                    int newSongDuration = songsToTryAdd.ToList()[0].Duration;
                    //ако надвиши максимума изцяло, взимаме друга песен
                    if (newSongDuration > allowedOffsetLess + allowedOffsetMore)
                    {
                        songsCount++;
                        var songsToTryAdd2 = incomingList.Skip(songsCount).Take(1);
                        if (songsToTryAdd2 == null || songsToTryAdd2.Count() == 0)
                        {
                            int[] offsets = { allowedOffsetLess -= secondsAllowed - songsDuration, allowedOffsetMore };

                            return (result.Values.ToList(), offsets);
                        }
                        continue;
                    }
                    else
                    {
                        //setvame pesenta, vliza v diapazona
                        var toAdd = songsToTryAdd.First();
                        result.Add(toAdd.Artist, toAdd);

                        if (newSongDuration >= allowedOffsetLess)
                        {
                            allowedOffsetLess = 0;
                            //TODO:              \/
                            allowedOffsetMore -= 300 - newSongDuration;
                        }
                        else
                        {
                            allowedOffsetLess -= newSongDuration;
                        }
                        int[] offsets = { allowedOffsetLess, allowedOffsetMore };


                        return (result.Values.ToList(), offsets);

                    }
                }

            }


            int[] arr = { allowedOffsetLess, allowedOffsetMore };

            return (result.Values.ToList(), arr);
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
