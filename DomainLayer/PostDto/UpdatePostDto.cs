

namespace DomainLayer.PostDto
{
    public class UpdatePostDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
