using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KudevolveWeb.Models
{
    public class PostResult
    {
        public string PostId { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public string Hashtag { get; set; }
        public string ip_address { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public AppUser Owner { get; set; }
        public string URL { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Int32 Likes { get; set; }
        public ICollection<AppUser> Followers { get; set; }

    }
}