using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PG.Services.Contract
{
    public interface IDeezerAPIService
    {
        Task ExtractSongsFromPlaylists(string genreString);
    }
}
