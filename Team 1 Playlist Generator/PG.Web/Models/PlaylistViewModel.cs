using PagedList;
using System.Collections.Generic;

namespace PG.Web.Models
{
    public class PlaylistViewModel
    {
        public int Id { get; set; }

        public List<int> DeezerIDs { get; set; }

        public string Title { get; set; }

        public int Duration { get; set; }

        public int Rank { get; set; }

        public string DurationInHours { get; set; }

        public int Fans { get; set; }

        public string PixabayImage { get; set; }

        public bool IsPublic { get; set; }

        public string UserId { get; set; }

        public string CreatorName { get; set; }

        public string LoggedUserId { get; set; }

        public ICollection<SongViewModel> Songs { get; set; } = new List<SongViewModel>();

        public IPagedList<SongViewModel> SongsPaged { get; set; }
    }
}
