using PG.Models;
using PG.Services.DTOs;

namespace PG.Services.Mappers
{
    public static class SongMapper
    {
        public static SongDTO ToDTO(this Song song)
        {
            return new SongDTO()
            {
                Id = song.Id,
                DeezerID = song.DeezerID,
                Title = song.Title,
                Duration = song.Duration,
                Rank = song.Rank,
                Preview = song.Preview,
                ArtistId = song.ArtistId,
                GenreId = song.GenreId,
                AlbumId = song.AlbumId,
            };
        }
        public static Song ToEntity(this SongDTO songDTO)
        {
            return new Song()
            {
                Id = songDTO.Id,
                DeezerID = songDTO.DeezerID,
                Title = songDTO.Title,
                Duration = songDTO.Duration,
                Rank = songDTO.Rank,
                Preview = songDTO.Preview,
                ArtistId = songDTO.ArtistId.GetValueOrDefault(),
                GenreId = songDTO.GenreId.GetValueOrDefault(),
                AlbumId = songDTO.AlbumId.GetValueOrDefault(),
            };
        }
    }
}
