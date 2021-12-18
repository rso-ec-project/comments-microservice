using Comments.Domain.Shared;
using System.Linq;
using System.Threading.Tasks;

namespace Comments.Application.Ratings
{
    public class RatingService : IRatingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RatingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RatingDto> GetAsync(int chargingStationId)
        {
            var comments = await _unitOfWork.CommentRepository.GetAsync();
            var chargingStationComments = comments.Where(x => x.ChargingStationId == chargingStationId).ToList();

            if (!chargingStationComments.Any())
                return null;

            return new RatingDto()
            {
                ChargingStationId = chargingStationId,
                Rating = chargingStationComments.Average(x => x.Rating),
                TotalRatingCount = chargingStationComments.Count,
                Rating1Count = chargingStationComments.Count(x => x.Rating == 1),
                Rating2Count = chargingStationComments.Count(x => x.Rating == 2),
                Rating3Count = chargingStationComments.Count(x => x.Rating == 3),
                Rating4Count = chargingStationComments.Count(x => x.Rating == 4),
                Rating5Count = chargingStationComments.Count(x => x.Rating == 5),
            };
        }
    }
}
