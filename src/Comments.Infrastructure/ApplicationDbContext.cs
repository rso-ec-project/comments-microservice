using Comments.Domain.CommentAggregate;
using Comments.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Comments.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.ApplyConfiguration(new CommentEntityTypeConfiguration());
        }
    }
}
