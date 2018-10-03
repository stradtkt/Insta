using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insta.Models
{
    public class Comment : BaseEntity
    {
        [Key]
        public int comment_id {get;set;}
        public string comment {get;set;}
        public int user_id {get;set;}
        public User User {get;set;}
        public int photo_id {get;set;}
        public virtual Photo Photo {get;set;}
        public Comment()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}