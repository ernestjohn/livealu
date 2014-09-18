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
        public string BIO { get; set; }
        public string password_reset_code { get; set; }
        public bool isGovernor { get; set; }
        public bool isActive { get; set; }
        public string activation_code { get; set; }


        //The New authentication 
        public string Facebook { get; set; }
        public string Google { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string LinkedIn { get; set; }

        public ICollection<AppUser> Friends { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Petition> Petitions { get; set; }
        public ICollection<Group> Groups { get; set; }
        
    }
}