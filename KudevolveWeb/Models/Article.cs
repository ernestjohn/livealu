using System.Collections.Generic;

namespace KudevolveWeb.Models
{
    public class Article
    {
        public string ArticleId { get; set; }
        public string ImageUrl { get; set; }
        public string URL { get; set; }
        public string Content { get; set; }
        public AppUser Owner { get; set; }
        public string DateCreated { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}