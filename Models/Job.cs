using System;
using System.ComponentModel.DataAnnotations;

namespace Insta.Models
{
    public class Job
    {
        [Key]
        public int job_id {get;set;}
        public int job_rating {get;set;}
        public string job_title {get;set;}
        public string company {get;set;}
        public DateTime to_date {get;set;}
        public DateTime from_date {get;set;}
        public string job_description {get;set;}
        public string skills {get;set;}
        public int user_id {get;set;}
        public User User {get;set;}
    }
}