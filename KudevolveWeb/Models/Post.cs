using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KudevolveWeb.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }
        public string Content { get; set; }
        public string DateCreated { get; set; }
        public string Hashtag { get; set; }
        public string ip_address { get; set; }
        public string latitude { get; set; }
        public string  longitude { get; set; }
        public bool isImage { get; set; }
        public AppUser Owner { get; set; }
        public string URL { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Int32 Likes { get; set; }
        public ICollection<AppUser> Followers { get; set; }

      
    }
}