namespace KudevolveWeb.Models
{
    public class Comment
    {
        public string CommentId { get; set; }
        public string PostUser { get; set; }
        public string Content { get; set; }
        public string Likes { get; set; }
    }
}