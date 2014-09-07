using System.Collections.Generic;

namespace KudevolveWeb.Models
{
    public class BlogPost
    {
        public string BlogPostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public string URL { get; set; }
        public string DateCreated { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public AppUser Owner { get; set; }
    }
}