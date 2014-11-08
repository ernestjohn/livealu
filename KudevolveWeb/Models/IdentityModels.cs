using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;

namespace KudevolveWeb.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
   

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("KudevolveContext")
        {
        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Group> Groups { get; set; }
        //public DbSet<County> Counties { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<Personality> Personalities { get; set; }
        public DbSet<Petition> Petitions { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

        

    }
}