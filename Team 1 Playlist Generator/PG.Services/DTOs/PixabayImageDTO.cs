using PG.Services.DTOs.Abstract;

namespace PG.Services.DTOs
{
    public class PixabayImageDTO : EntityDTO
    {
        public string PreviewURL { get; set; }

        public string WebformatURL { get; set; }

        public string LargeImageURL { get; set; }
    }
}
