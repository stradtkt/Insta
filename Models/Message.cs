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
        public int from_id {get;set;}
        public User From {get;set;}
        public int to_id {get;set;}
        public User To {get;set;}
        public int reply_to {get;set;}
        public Message Reply {get;set;}
        public List<Message> Replies {get;set;}
        public Message()
        {
            Replies = new List<Message>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}