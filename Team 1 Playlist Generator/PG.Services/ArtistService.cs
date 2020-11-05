using PG.Services.Contract;
using PG.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PG.Services
{
    public class ArtistService : IArtistService
    {
        public Task<ArtistDTO> Create(ArtistDTO ArtistDTO)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ArtistDTO>> GetAllArtists()
        {
            throw new System.NotImplementedException();
        }

        public Task<ArtistDTO> GetArtistById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ArtistDTO> Update(int id, ArtistDTO artistDTO)
        {
            throw new System.NotImplementedException();
        }
    }
}
