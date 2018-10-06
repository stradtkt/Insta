using System.ComponentModel.DataAnnotations;

namespace Insta.Models
{
    public class Friends 
    {
        [Key]
        public int user_friend_id {get;set;}
        public int user_id {get;set;}
        public User User {get;set;}
        public int friend_id {get;set;}
        public Friend Friend {get;set;}
    }
}