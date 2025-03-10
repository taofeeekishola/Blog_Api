﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Model
{
    public class Post : BaseModel
    {
        public string Title { get; set; }

        //setting the table to varchar in the database
        [Column(TypeName = "varchar(1000)")]
        public string Content { get; set; }

        [ForeignKey("UserId")]
        public User user { get; set; }
        public string UserId { get; set; }
    }
}
