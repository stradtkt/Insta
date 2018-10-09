using System.ComponentModel.DataAnnotations;
using System;
namespace Insta.Models
{
    public class RequestFriend
    {
        [Key]
        public int friend_id {get;set;}

        public Byte is_friend {get;set;}
        public Byte requested {get;set;}
        public Byte accepted_request {get;set;}
        public RequestFriend()
        {
            is_friend = 0;
            requested = 1;
            accepted_request = 0;
        }
    }
    public class AcceptRequest : BaseEntity
    {
         public int friend_id {get;set;}
        public Byte is_friend {get;set;}
        public Byte requested {get;set;}
        public Byte accepted_request {get;set;}
        public AcceptRequest()
        {
            is_friend = 1;
            requested = 0;
            accepted_request = 1;
        }
        public class RejectRequest
        {
            public int friend_id {get;set;}
            public Byte is_friend {get;set;}
            public Byte requested {get;set;}
            public Byte accepted_request {get;set;}
            public RejectRequest()
            {
                is_friend = 0;
                requested = 0;
                accepted_request = 0;
            }
        }
    }

}