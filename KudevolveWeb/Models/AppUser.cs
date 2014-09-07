using System.Collections.Generic;

namespace KudevolveWeb.Models
{
    public class AppUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string County { get; set; }
        public string URL { get; set; }
        public string DateOfBirth { get; set; }
        public string ImageUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<AppUser> Friends { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Petition> Petitions { get; set; }
        public ICollection<Group> Groups { get; set; }
        
    }
}