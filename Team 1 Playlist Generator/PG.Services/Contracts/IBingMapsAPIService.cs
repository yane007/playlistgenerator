using System.Threading.Tasks;

namespace PG.Services.Contract
{
    public interface IBingMapsAPIService
    {
        Task<int> FindDuration(string start, string end);
    }
}
