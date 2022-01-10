using Comments.Application.Comments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Comments.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Get a list of comments for a single charging station.
        /// </summary>
        /// <param name="chargingStationId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<CommentDto>>> GetByChargingStation([FromQuery, Required] int chargingStationId, [FromQuery] int? userId = null)
        {
            return await _commentService.GetByChargingStationAsync(chargingStationId, userId);
        }

        /// <summary>
        /// Get a single comment by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CommentDto>> Get(int id)
        {
            var comment = await _commentService.GetAsync(id);

            if (comment == null)
                return NotFound();

            return comment;
        }

        /// <summary>
        /// Create a comment.
        /// </summary>
        /// <param name="commentPostDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CommentDto>> Post([FromBody] CommentPostDto commentPostDto)
        {
            return await _commentService.PostAsync(commentPostDto);
        }

        /// <summary>
        /// Update a comment.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reservationPutDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CommentDto>> Put(int id, [FromBody] CommentPutDto reservationPutDto)
        {
            var comment = await _commentService.PutAsync(id, reservationPutDto);

            if (comment == null)
                return NotFound();

            return comment;
        }

        /// <summary>
        /// Delete a comment.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            var isDeleted = await _commentService.DeleteAsync(id);

            if (!isDeleted)
                return NotFound();
            return Ok();
        }
    }
}
