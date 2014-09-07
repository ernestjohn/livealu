using System;
using System.Collections.Generic;

namespace KudevolveWeb.Models
{
    public class Petition
    {
      
        public string PetitionId { get; set; }
        public string Name { get; set; }
        public string DateCreated { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public Int32 Votes { get; set; }
        public ICollection<AppUser> Voters { get; set; }
        public string Status { get; set; }
        public string Hashtag { get; set; }
        public string Likes { get; set; }
        public AppUser Owner { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public string URL { get; set; }
    }
}