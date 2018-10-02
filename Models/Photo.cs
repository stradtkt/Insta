using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Insta.Models
{
    public class Photo : BaseEntity
    {
        [Key]
        public int photo_id {get;set;}

        public string image {get;set;}
        public string img_alt {get;set;}
        public string description {get;set;}
        public int user_id {get;set;}
        public User User {get;set;}
        public List<Comment> Comments {get;set;}
        public List<Like> Likes {get;set;}
        public Photo()
        {
            Likes = new List<Like>();
            Comments = new List<Comment>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }    
}