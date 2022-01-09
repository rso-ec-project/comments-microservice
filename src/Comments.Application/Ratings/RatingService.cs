using Comments.Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Comments.Application.Ratings
{
    public class RatingService : IRatingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RatingService> _logger;

        public RatingService(IUnitOfWork unitOfWork, ILogger<RatingService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<RatingDto> GetAsync(int chargingStationId)
        {
            var endpoint = $"endpoint GET /Comments?chargingStationId={chargingStationId}";
            try
            {
                _logger.LogInformation($"Entered {endpoint}");
                var comments = await _unitOfWork.CommentRepository.GetAsync();
                var chargingStationComments = comments.Where(x => x.ChargingStationId == chargingStationId).ToList();

                if (!chargingStationComments.Any())
                {
                    _logger.LogInformation($"Exited {endpoint} with: 404 ChargingStation with Id {chargingStationId} not found");
                    return null;
                }

                var ratingDto = new RatingDto()
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
                _logger.LogInformation($"Exited {endpoint} with: 200 OK");
                return ratingDto;
            }
            catch (Exception e)
            {
                _logger.LogError($"Exited {endpoint} with: Exception {e}");
                throw;
            }
        }
    }
}
