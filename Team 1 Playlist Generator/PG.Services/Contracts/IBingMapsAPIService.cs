using System.Threading.Tasks;

namespace PG.Services.Contract
{
    public interface IBingMapsAPIService
    {
        /// <summary>
        /// Finds the travel duration between two locations.
        /// </summary>
        /// <param name="start">First location</param>
        /// <param name="end">Second location</param>
        /// <returns>Returns the travel duration in seconds</returns>
        Task<int> FindDuration(string start, string end);
    }
}
