using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insta.Models
{
    public class AddProfile
    {
        [Key]
        public int user_id {get;set;}
        [Required(ErrorMessage="Profile Image is required")]
        [Display(Name="Profile Image")]
        public string profile_img {get;set;}
        [Required(ErrorMessage="Background Image is required")]
        [Display(Name="Background Image")]
        public string background_img {get;set;}
        [MinLength(2, ErrorMessage="Location has a min length of 2")]
        [MaxLength(100, ErrorMessage="Locations has a max length of 100")]
        public string location {get;set;}
        [MinLength(2, ErrorMessage="Occupation has a min length of 2")]
        [MaxLength(100, ErrorMessage="Occupation has a max length of 100")]
        public string occupation {get;set;}
        [MinLength(2, ErrorMessage="Skills has a min length of 2")]
        public string skills {get;set;}
        [MinLength(2, ErrorMessage="Recent jobs has a min length of 2")]
        public string recent_jobs {get;set;}
    }
}