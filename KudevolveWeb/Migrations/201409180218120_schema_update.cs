namespace KudevolveWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class schema_update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppUsers", "BIO", c => c.String());
            AddColumn("dbo.AppUsers", "password_reset_code", c => c.String());
            AddColumn("dbo.AppUsers", "isGovernor", c => c.Boolean(nullable: false));
            AddColumn("dbo.AppUsers", "isActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.AppUsers", "activation_code", c => c.String());
            AddColumn("dbo.AppUsers", "Facebook", c => c.String());
            AddColumn("dbo.AppUsers", "Google", c => c.String());
            AddColumn("dbo.AppUsers", "Instagram", c => c.String());
            AddColumn("dbo.AppUsers", "Twitter", c => c.String());
            AddColumn("dbo.AppUsers", "LinkedIn", c => c.String());
            AddColumn("dbo.Posts", "ip_address", c => c.String());
            AddColumn("dbo.Posts", "latitude", c => c.String());
            AddColumn("dbo.Posts", "longitude", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "longitude");
            DropColumn("dbo.Posts", "latitude");
            DropColumn("dbo.Posts", "ip_address");
            DropColumn("dbo.AppUsers", "LinkedIn");
            DropColumn("dbo.AppUsers", "Twitter");
            DropColumn("dbo.AppUsers", "Instagram");
            DropColumn("dbo.AppUsers", "Google");
            DropColumn("dbo.AppUsers", "Facebook");
            DropColumn("dbo.AppUsers", "activation_code");
            DropColumn("dbo.AppUsers", "isActive");
            DropColumn("dbo.AppUsers", "isGovernor");
            DropColumn("dbo.AppUsers", "password_reset_code");
            DropColumn("dbo.AppUsers", "BIO");
        }
    }
}
