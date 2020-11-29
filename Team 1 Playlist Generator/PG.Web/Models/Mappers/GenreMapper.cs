using PG.Services.DTOs;

namespace PG.Web.Models.Mappers
{
    public static class GenreMapper
    {
        public static GenreViewModel ToViewModel(this GenreDTO genreDTO)
        {
            return new GenreViewModel()
            {
                Name = genreDTO.Name,
            };
        }

        public static GenreDTO ToDTO(this GenreViewModel genreDTO)
        {
            return new GenreDTO()
            {
                Name = genreDTO.Name,
            };
        }
    }
}
