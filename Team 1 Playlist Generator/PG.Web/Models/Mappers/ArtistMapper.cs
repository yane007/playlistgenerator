using PG.Services.DTOs;

namespace PG.Web.Models.Mappers
{
    public static class ArtistMapper
    {
        public static ArtistViewModel ToViewModel(this ArtistDTO artistDTO)
        {
            return new ArtistViewModel()
            {
                Name = artistDTO.Name,
                Tracklist = artistDTO.Tracklist,
                Type = artistDTO.Type
            };
        }

        public static ArtistDTO ToDTO(this ArtistViewModel artistViewModel)
        {
            return new ArtistDTO()
            {
                Name = artistViewModel.Name,
                Tracklist = artistViewModel.Tracklist,
                Type = artistViewModel.Type
            };
        }
    }
}
