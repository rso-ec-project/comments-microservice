using Comments.Domain.Shared;

namespace Comments.Domain.CommentAggregate
{
    public interface ICommentRepository : IRepository<Comment, int>
    {
    }
}
