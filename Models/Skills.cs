using System;
using System.ComponentModel.DataAnnotations;

namespace Insta.Models
{
    public class Skills : BaseEntity 
    {
        [Key]
        public int skill_id {get;set;}
        public string skill_title {get;set;}
        public int skill_level {get;set;}
        public string skill_description {get;set;}
        public int user_id {get;set;}
        public User User {get;set;}
        public Skills()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}