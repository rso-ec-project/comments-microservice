namespace Comments.Application.Comments
{
    public class CommentPostDto
    {
        public string Content { get; set; }
        public int Rating { get; set; }
        public int UserId { get; set; }
        public int ChargingStationId { get; set; }
    }
}
