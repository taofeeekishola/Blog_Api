

namespace DomainLayer.CommentDto
{
    public class UpdateCommentDto
    {
       
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; }
    }
}
