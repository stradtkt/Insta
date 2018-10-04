using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insta.Models
{
    public class Like
    {
        [Key]
        public int like_id {get;set;}
        public int user_id {get;set;}
         public virtual User User {get;set;}
        public int photo_id {get;set;}
        public virtual Photo Photo {get;set;}
    }
}