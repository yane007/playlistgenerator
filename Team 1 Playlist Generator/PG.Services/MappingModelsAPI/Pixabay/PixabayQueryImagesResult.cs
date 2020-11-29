namespace PG.Services.MappingModelsAPI.Pixabay
{
    public class PixabayQueryImagesResult
    {
        public int total { get; set; }
        public int totalHits { get; set; }
        public PixabayImageAPI[] hits { get; set; }
    }
}
