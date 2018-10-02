using System.ComponentModel.DataAnnotations;

namespace Insta.Models
{
    public class UsersMessages
    {
        [Key]
        public int user_message_id {get;set;}
        public User User {get;set;}
        public Message Message {get;set;}
        public int user_id {get;set;}
        public int message_id {get;set;}
    }
}