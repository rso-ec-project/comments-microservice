using Comments.Domain.Shared.Entities;
using System;

namespace Comments.Domain.CommentAggregate
{
    public class Comment : Entity<int>
    {
        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public int ChargingStationId { get; set; }
    }
}
