using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Insta.Models
{
    public class Comment : BaseEntity
    {
        [Key]
        public int comment_id {get;set;}
        public string comment {get;set;}
        public List<Like> Likes {get;set;}
        public Comment()
        {
            Likes = new List<Like>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}