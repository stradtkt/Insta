using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Insta.Models
{
    public class Message : BaseEntity
    {
        [Key]
        public int message_id {get;set;}
        public string message {get;set;}
        public List<UsersMessages> UsersMessages {get;set;}
        public Message()
        {
            UsersMessages = new List<UsersMessages>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}