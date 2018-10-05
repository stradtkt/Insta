using System;
using System.ComponentModel.DataAnnotations;

namespace Insta.Models
{
    public class AddJob
    {
        [Key]
        public int job_id {get;set;}
        [Required(ErrorMessage="Job Rating is required")]
        [Display(Name="Job Rating")]
        public int job_rating {get;set;}
        [Required(ErrorMessage="Job title is required")]
        [MinLength(2, ErrorMessage="Job title has a min length of 2")]
        [MaxLength(40, ErrorMessage="Job title has a max length of 40")]
        [Display(Name="Job Title")]
        public string job_title {get;set;}
        [Required(ErrorMessage="Company is required")]
        [MinLength(2, ErrorMessage="Company has a min length of 2")]
        [MaxLength(40, ErrorMessage="Company has a max length of 40")]
        [Display(Name="Company")]
        public string company {get;set;}
        [Required(ErrorMessage="To date is required")]
        [DataType(DataType.Date)]
        [Display(Name="To Date")]
        public DateTime to_date {get;set;}
        [Required(ErrorMessage="From date is required")]
        [DataType(DataType.Date)]
        [Display(Name="From Date")]
        public DateTime from_date {get;set;}
        [Required(ErrorMessage="Description is required")]
        [MinLength(5, ErrorMessage="Description has a min length of 5")]
        [Display(Name="Job Description")]
        public string job_description {get;set;}
        [Required(ErrorMessage="Skills is required")]
        [MinLength(5, ErrorMessage="Skills has a min length of 5")]
        [Display(Name="Skills")]
        public string skills {get;set;}
    }
}