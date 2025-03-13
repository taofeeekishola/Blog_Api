﻿
namespace DomainLayer.CommentDto
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
    }
}
