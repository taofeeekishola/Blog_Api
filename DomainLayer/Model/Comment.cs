﻿
using System.ComponentModel.DataAnnotations.Schema;


namespace DomainLayer.Model
{
    public class Comment : BaseModel
    {
        //setting the table to varchar in the database
        [Column(TypeName = "varchar(200)")]
        public string Content { get; set; }

        [ForeignKey("PostId")]
        public Post post { get; set; }
        public int PostId { get; set; }

        [ForeignKey("UserId")]
        public User user { get; set; }
        public string UserId { get; set; }
    }
}
