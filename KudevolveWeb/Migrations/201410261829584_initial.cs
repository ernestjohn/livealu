namespace KudevolveWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ArticleId = c.String(nullable: false, maxLength: 128),
                        ImageUrl = c.String(),
                        URL = c.String(),
                        Content = c.String(),
                        DateCreated = c.String(),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ArticleId)
                .ForeignKey("dbo.AppUsers", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.String(nullable: false, maxLength: 128),
                        PostUser = c.String(),
                        Content = c.String(),
                        Likes = c.String(),
                        Article_ArticleId = c.String(maxLength: 128),
                        Post_PostId = c.Int(),
                        Petition_PetitionId = c.String(maxLength: 128),
                        BlogPost_BlogPostId = c.String(maxLength: 128),
                        Suggestion_SuggestionId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Articles", t => t.Article_ArticleId)
                .ForeignKey("dbo.Posts", t => t.Post_PostId)
                .ForeignKey("dbo.Petitions", t => t.Petition_PetitionId)
                .ForeignKey("dbo.BlogPosts", t => t.BlogPost_BlogPostId)
                .ForeignKey("dbo.Suggestions", t => t.Suggestion_SuggestionId)
                .Index(t => t.Article_ArticleId)
                .Index(t => t.Post_PostId)
                .Index(t => t.Petition_PetitionId)
                .Index(t => t.BlogPost_BlogPostId)
                .Index(t => t.Suggestion_SuggestionId);
            
            CreateTable(
                "dbo.AppUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        FirstName = c.String(),
                        SecondName = c.String(),
                        County = c.String(),
                        URL = c.String(),
                        DateOfBirth = c.String(),
                        ImageUrl = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        BIO = c.String(),
                        password_reset_code = c.String(),
                        isGovernor = c.Boolean(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        activation_code = c.String(),
                        Facebook = c.String(),
                        Google = c.String(),
                        Instagram = c.String(),
                        Twitter = c.String(),
                        LinkedIn = c.String(),
                        PersonalityId = c.String(),
                        Likes = c.String(),
                        Position = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        AppUser_Id = c.String(maxLength: 128),
                        Group_Id = c.String(maxLength: 128),
                        Post_PostId = c.Int(),
                        Petition_PetitionId = c.String(maxLength: 128),
                        Comments_CommentId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUsers", t => t.AppUser_Id)
                .ForeignKey("dbo.Groups", t => t.Group_Id)
                .ForeignKey("dbo.Posts", t => t.Post_PostId)
                .ForeignKey("dbo.Petitions", t => t.Petition_PetitionId)
                .ForeignKey("dbo.Comments", t => t.Comments_CommentId)
                .Index(t => t.AppUser_Id)
                .Index(t => t.Group_Id)
                .Index(t => t.Post_PostId)
                .Index(t => t.Petition_PetitionId)
                .Index(t => t.Comments_CommentId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        DateFormed = c.String(),
                        Owner_Id = c.String(maxLength: 128),
                        AppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUsers", t => t.Owner_Id)
                .ForeignKey("dbo.AppUsers", t => t.AppUser_Id)
                .Index(t => t.Owner_Id)
                .Index(t => t.AppUser_Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        DateCreated = c.String(),
                        Hashtag = c.String(),
                        ip_address = c.String(),
                        latitude = c.String(),
                        longitude = c.String(),
                        isImage = c.Boolean(nullable: false),
                        URL = c.String(),
                        Likes = c.Int(nullable: false),
                        Owner_Id = c.String(maxLength: 128),
                        Group_Id = c.String(maxLength: 128),
                        AppUser_Id = c.String(maxLength: 128),
                        Discussion_DiscussionId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.AppUsers", t => t.Owner_Id)
                .ForeignKey("dbo.Groups", t => t.Group_Id)
                .ForeignKey("dbo.AppUsers", t => t.AppUser_Id)
                .ForeignKey("dbo.Discussions", t => t.Discussion_DiscussionId)
                .Index(t => t.Owner_Id)
                .Index(t => t.Group_Id)
                .Index(t => t.AppUser_Id)
                .Index(t => t.Discussion_DiscussionId);
            
            CreateTable(
                "dbo.Petitions",
                c => new
                    {
                        PetitionId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        DateCreated = c.String(),
                        Content = c.String(),
                        Category = c.String(),
                        Votes = c.Int(nullable: false),
                        Status = c.String(),
                        Hashtag = c.String(),
                        Likes = c.String(),
                        URL = c.String(),
                        Owner_Id = c.String(maxLength: 128),
                        AppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PetitionId)
                .ForeignKey("dbo.AppUsers", t => t.Owner_Id)
                .ForeignKey("dbo.AppUsers", t => t.AppUser_Id)
                .Index(t => t.Owner_Id)
                .Index(t => t.AppUser_Id);
            
            CreateTable(
                "dbo.BlogPosts",
                c => new
                    {
                        BlogPostId = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        Content = c.String(),
                        ImageUrl = c.String(),
                        URL = c.String(),
                        DateCreated = c.String(),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BlogPostId)
                .ForeignKey("dbo.AppUsers", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Discussions",
                c => new
                    {
                        DiscussionId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        URL = c.String(),
                        DateCreated = c.String(),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DiscussionId)
                .ForeignKey("dbo.AppUsers", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Suggestions",
                c => new
                    {
                        SuggestionId = c.String(nullable: false, maxLength: 128),
                        Content = c.String(),
                        URL = c.String(),
                        DateCreated = c.String(),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SuggestionId)
                .ForeignKey("dbo.AppUsers", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Suggestions", "Owner_Id", "dbo.AppUsers");
            DropForeignKey("dbo.Comments", "Suggestion_SuggestionId", "dbo.Suggestions");
            DropForeignKey("dbo.Posts", "Discussion_DiscussionId", "dbo.Discussions");
            DropForeignKey("dbo.Discussions", "Owner_Id", "dbo.AppUsers");
            DropForeignKey("dbo.BlogPosts", "Owner_Id", "dbo.AppUsers");
            DropForeignKey("dbo.Comments", "BlogPost_BlogPostId", "dbo.BlogPosts");
            DropForeignKey("dbo.Articles", "Owner_Id", "dbo.AppUsers");
            DropForeignKey("dbo.AppUsers", "Comments_CommentId", "dbo.Comments");
            DropForeignKey("dbo.Posts", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.Petitions", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.AppUsers", "Petition_PetitionId", "dbo.Petitions");
            DropForeignKey("dbo.Petitions", "Owner_Id", "dbo.AppUsers");
            DropForeignKey("dbo.Comments", "Petition_PetitionId", "dbo.Petitions");
            DropForeignKey("dbo.Groups", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.Posts", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Posts", "Owner_Id", "dbo.AppUsers");
            DropForeignKey("dbo.AppUsers", "Post_PostId", "dbo.Posts");
            DropForeignKey("dbo.Comments", "Post_PostId", "dbo.Posts");
            DropForeignKey("dbo.Groups", "Owner_Id", "dbo.AppUsers");
            DropForeignKey("dbo.AppUsers", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.AppUsers", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.Comments", "Article_ArticleId", "dbo.Articles");
            DropIndex("dbo.Suggestions", new[] { "Owner_Id" });
            DropIndex("dbo.Discussions", new[] { "Owner_Id" });
            DropIndex("dbo.BlogPosts", new[] { "Owner_Id" });
            DropIndex("dbo.Petitions", new[] { "AppUser_Id" });
            DropIndex("dbo.Petitions", new[] { "Owner_Id" });
            DropIndex("dbo.Posts", new[] { "Discussion_DiscussionId" });
            DropIndex("dbo.Posts", new[] { "AppUser_Id" });
            DropIndex("dbo.Posts", new[] { "Group_Id" });
            DropIndex("dbo.Posts", new[] { "Owner_Id" });
            DropIndex("dbo.Groups", new[] { "AppUser_Id" });
            DropIndex("dbo.Groups", new[] { "Owner_Id" });
            DropIndex("dbo.AppUsers", new[] { "Comments_CommentId" });
            DropIndex("dbo.AppUsers", new[] { "Petition_PetitionId" });
            DropIndex("dbo.AppUsers", new[] { "Post_PostId" });
            DropIndex("dbo.AppUsers", new[] { "Group_Id" });
            DropIndex("dbo.AppUsers", new[] { "AppUser_Id" });
            DropIndex("dbo.Comments", new[] { "Suggestion_SuggestionId" });
            DropIndex("dbo.Comments", new[] { "BlogPost_BlogPostId" });
            DropIndex("dbo.Comments", new[] { "Petition_PetitionId" });
            DropIndex("dbo.Comments", new[] { "Post_PostId" });
            DropIndex("dbo.Comments", new[] { "Article_ArticleId" });
            DropIndex("dbo.Articles", new[] { "Owner_Id" });
            DropTable("dbo.Suggestions");
            DropTable("dbo.Discussions");
            DropTable("dbo.BlogPosts");
            DropTable("dbo.Petitions");
            DropTable("dbo.Posts");
            DropTable("dbo.Groups");
            DropTable("dbo.AppUsers");
            DropTable("dbo.Comments");
            DropTable("dbo.Articles");
        }
    }
}
