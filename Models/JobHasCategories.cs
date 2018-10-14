using System.ComponentModel.DataAnnotations;

namespace Insta.Models
{
    public class JobsHasCategories 
    {
        [Key]
        public int category_list_id {get;set;}
        public int list_id {get;set;}
        public int category_id {get;set;}
        public JobCategory JobCategory {get;set;}
        public Jobs Jobs {get;set;}
    }
}