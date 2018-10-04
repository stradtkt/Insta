using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Insta.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int user_id {get;set;}
        public string first_name {get;set;}
        public string last_name {get;set;}
        public string email {get;set;}
        public string password {get;set;}
        public string phone {get;set;}
        public string occupation {get;set;}
        public string location {get;set;}
        public string skills {get;set;}
        public string recent_jobs {get;set;}
        public string profile_img {get;set;}
        public string background_img {get;set;}
        public List<Photo> Photos {get;set;}
        public List<Message> Tos {get;set;}
        public List<Comment> Comments {get;set;}
        public IEnumerable<Like> Likes {get;set;}
        public List<Message> Froms {get;set;}
        public User()
        {
            Photos = new List<Photo>();
            Comments = new List<Comment>();
            Froms = new List<Message>();
            Tos = new List<Message>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }   
}