using System.ComponentModel.DataAnnotations;

namespace Insta.Models
{
    public class AddPhoto : BaseEntity
    {
        [Key]
        public int photo_id {get;set;}
        [Required(ErrorMessage="Image is required")]
        [DataType(DataType.Upload)]
        [Display(Name="Image")]
        public string image {get;set;}

        [MinLength(2, ErrorMessage="There needs to be at least 2 characters for the image alt")]
        [MaxLength(100, ErrorMessage="There is a max length of 100 for the image alt")]
        public string img_alt {get;set;}

        [MinLength(5, ErrorMessage="Description has a min length of 5")]
        [MaxLength(1000, ErrorMessage="Description has a max length of 1000")]
        [DataType(DataType.Text)]
        [Display(Name="Description")]
        public string description {get;set;}
    }
}