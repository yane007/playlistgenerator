using PG.Services.DTOs.Abstract;

namespace PG.Services.DTOs
{
    public class GenreDTO : IdAndDeletedDTO
    {
        public string Name { get; set; }
    }
}
