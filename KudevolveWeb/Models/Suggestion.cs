using System.Collections.Generic;

namespace KudevolveWeb.Models
{
    public class Suggestion
    {
        public string SuggestionId { get; set; }
        public string Content { get; set; }
        public string URL { get; set; }
        public string DateCreated { get; set; }
        public AppUser Owner { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}