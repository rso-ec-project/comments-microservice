using Comments.Domain.CommentAggregate;
using System.Threading.Tasks;

namespace Comments.Domain.Shared
{
    public interface IUnitOfWork
    {
        IUnitOfWork CreateContext();

        ICommentRepository CommentRepository { get; }

        Task<int> CommitAsync();
    }
}
