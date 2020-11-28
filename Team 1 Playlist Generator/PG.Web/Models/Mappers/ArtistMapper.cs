using PG.Services.DTOs;

namespace PG.Web.Models.Mappers
{
    public static class ArtistMapper
    {
        public static ArtistViewModel ToViewModel(this ArtistDTO artistDTO)
        {
            return new ArtistViewModel()
            {
                Id = artistDTO.Id,
                Name = artistDTO.Name,
                Tracklist = artistDTO.Tracklist,
                Type = artistDTO.Type
            };
        }

        public static ArtistDTO ToDTO(this ArtistViewModel artistViewModel)
        {
            return new ArtistDTO()
            {
                Id = artistViewModel.Id,
                Name = artistViewModel.Name,
                Tracklist = artistViewModel.Tracklist,
                Type = artistViewModel.Type
            };
        }
    }
}
