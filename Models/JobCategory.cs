using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Insta.Models
{
    public class JobCategory 
    {
        [Key]
        public int category_id {get;set;}
        public string category_name {get;set;}
        public string category_image {get;set;}
        public List<JobsHasCategories> JobsCategories {get;set;}
        public JobCategory()
        {
            JobsCategories = new List<JobsHasCategories>();
        }
    }
}