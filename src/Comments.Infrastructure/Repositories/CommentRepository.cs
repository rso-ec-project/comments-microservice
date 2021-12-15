using Comments.Domain.CommentAggregate;

namespace Comments.Infrastructure.Repositories
{
    public class CommentRepository : Repository<Comment, int>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
