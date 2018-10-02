using System.ComponentModel.DataAnnotations;

namespace Insta.Models
{
    public class Like
    {
        [Key]
        public int like_id {get;set;}
        public int user_id {get;set;}
        public int photo_id {get;set;}
        public int comment_id {get;set;}
        public User User {get;set;}
        public Photo Photo {get;set;}
        public Comment Comment {get;set;}
    }
}