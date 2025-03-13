
using System.ComponentModel.DataAnnotations.Schema;


namespace DomainLayer.Model
{
    public class Like : BaseModel
    {
        [ForeignKey("PostId")]
        public Post post { get; set; }
        public int PostId { get; set; }

        [ForeignKey("UserId")]
        public User user { get; set; }
        public string UserId { get; set; }
    }
}
