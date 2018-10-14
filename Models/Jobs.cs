using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Insta.Models
{
    public class Jobs : BaseEntity
    {
        [Key]
        public int list_id {get;set;}
        public string job_name {get;set;}
        public string job_credentials {get;set;}
        public string job_description {get;set;}
        public string job_requirements {get;set;}
        public string job_perks {get;set;}
        public string contact_company {get;set;}
        public string contact_phone {get;set;}
        public string contact_fax {get;set;}
        public string contact_person {get;set;}
        public string contact_address {get;set;}
        public List<JobsHasCategories> JobsCategories {get;set;}
        public Jobs()
        {
            JobsCategories = new List<JobsHasCategories>();
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}