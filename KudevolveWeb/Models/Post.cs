using System;
using System.Collections.Generic;

namespace KudevolveWeb.Models
{
    public class Post
    {
        public string PostId { get; set; }
        public string Content { get; set; }
        public string DateCreated { get; set; }
        public string Hashtag { get; set; }
        public AppUser Owner { get; set; }
        public string URL { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Int32 Likes { get; set; }
        public ICollection<AppUser> Followers { get; set; }

      
    }
}