using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comments.Application.Comments
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetAsync();
        Task<CommentDto> GetAsync(int commentId);
    }
}
