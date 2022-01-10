using Comments.Application.Ratings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Comments.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingsController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        /// <summary>
        /// Get a summary of ratings for a single charging station.
        /// </summary>
        /// <param name="chargingStationId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RatingDto>> Get([FromQuery, Required] int chargingStationId)
        {
            var ratingDto = await _ratingService.GetAsync(chargingStationId);

            if (ratingDto == null)
                return NotFound();
            return Ok(ratingDto);
        }
    }
}
