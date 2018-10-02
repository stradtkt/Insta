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
    public class CommentOnPhoto : BaseEntity
    {
        [Key]
        public int comment_id {get;set;}
        [Required(ErrorMessage="Comment field is required to post something")]
        [MinLength(3, ErrorMessage="There is a min length of 3 for the comment")]
        [MaxLength(500, ErrorMessage="There is a max length of 500 for the comment")]
        [DataType(DataType.Text)]
        [Display(Name="Comment")]
        public string comment {get;set;}
        public int photo_id {get;set;}
    }
    public class LikeAPhoto
    {
        [Key]
        public int like_id {get;set;}
        public int photo_id {get;set;}
        public int user_id {get;set;}
    }
}