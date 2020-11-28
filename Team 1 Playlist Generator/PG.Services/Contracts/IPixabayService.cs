using System.Threading.Tasks;

namespace PG.Services
{
    public interface IPixabayService
    {
        Task<string> GetPixabayImage(int queryId);
    }
}