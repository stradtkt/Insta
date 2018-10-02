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
        public List<Photo> Photos {get;set;}
        public List<Message> Messages {get;set;}
        public List<Comment> Comments {get;set;}
        public List<Like> Likes {get;set;}
        public List<UsersMessages> UsersMessages {get;set;}
        public User()
        {
            Photos = new List<Photo>();
            UsersMessages = new List<UsersMessages>();
            Likes = new List<Like>();
            Comments = new List<Comment>();
            Messages = new List<Message>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }   
}