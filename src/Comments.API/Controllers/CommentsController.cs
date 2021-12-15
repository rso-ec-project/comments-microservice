using Comments.Application.Comments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comments.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<CommentDto>>> Get()
        {
            return await _commentService.GetAsync();
        }

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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CommentDto>> Post([FromBody] CommentPostDto commentPostDto)
        {
            return await _commentService.PostAsync(commentPostDto);
        }

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
    }
}
