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
        public int is_active {get;set;}
        public int is_viewed {get;set;}
        public Message()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}