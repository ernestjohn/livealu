using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KudevolveWeb.Models
{
    public class Group
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DateFormed { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<AppUser> Members { get; set; }
        public AppUser Owner { get; set; }
    }
}