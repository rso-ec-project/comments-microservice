using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comments.Application.Comments
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetByChargingStationAsync(int chargingStationId, int? userId = null);
        Task<CommentDto> GetAsync(int commentId);
        Task<CommentDto> PostAsync(CommentPostDto commentPostDto);
        Task<CommentDto> PutAsync(int commentId, CommentPutDto commentPutDto);
        Task<bool> DeleteAsync(int commentId);
    }
}
