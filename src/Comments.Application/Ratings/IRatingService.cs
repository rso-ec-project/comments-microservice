using System.Threading.Tasks;

namespace Comments.Application.Ratings
{
    public interface IRatingService
    {
        Task<RatingDto> GetAsync(int chargingStationId);
    }
}
