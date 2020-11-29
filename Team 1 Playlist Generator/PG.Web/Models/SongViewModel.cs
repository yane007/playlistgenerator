namespace PG.Web.Models
{
    public class SongViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Duration { get; set; }

        public string DurationInMinutes { get; set; }

        public int Rank { get; set; }

        public string Preview { get; set; }
    }
}