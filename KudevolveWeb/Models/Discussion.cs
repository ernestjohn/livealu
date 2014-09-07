using System.Collections.Generic;

namespace KudevolveWeb.Models
{
    public class Discussion
    {
        public string DiscussionId { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string DateCreated { get; set; }
        public AppUser Owner { get; set; }
        public List<Post> Posts { get; set; }
    }
}