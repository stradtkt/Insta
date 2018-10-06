using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Insta.Models
{
    public class Friend : BaseEntity
    {
        [Key]
        public int friend_id {get;set;}
        public Byte is_friend {get;set;}
        public Byte requested {get;set;}
        public Byte accepted_request {get;set;}
        public List<Friends> Friends {get;set;}
        public Friend()
        {
            Friends = new List<Friends>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}