namespace KudevolveWeb.Models
{
    public class Personality: AppUser
    {
        public string PersonalityId { get; set; }
        public string Likes { get; set; }
        public string Position { get; set; }
        public Comment Comments { get; set; }
    }
}