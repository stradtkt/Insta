using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Insta.Models
{
    public class Friend : BaseEntity
    {
        [Key]
        public int friend_id {get;set;}
        public int user_one_id {get;set;}
        public int user_two_id {get;set;}
        public int active_user_id {get;set;}
        public Byte is_friend {get;set;}
        public Byte requested {get;set;}
        public Byte accepted_request {get;set;}
        public Friend()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}