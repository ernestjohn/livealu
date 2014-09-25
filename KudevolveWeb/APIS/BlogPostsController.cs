using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using KudevolveWeb.Models;

namespace KudevolveWeb.APIS
{
    [RoutePrefix("api/vi/blogposts")]
    public class BlogPostsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public string BaseUrl = "http://kudevolve.azurewebsites.net";

        // GET: api/BlogPosts
        [HttpGet]
        [Route("")]
        public IQueryable<BlogPost> GetBlogPosts()
        {
            return db.BlogPosts.Include(p => p.Owner).Include(p => p.Comments);
        }

        // GET: api/BlogPosts/5
        [Route("{id}")]
        [HttpGet]
        [ResponseType(typeof(BlogPost))]
        public IHttpActionResult GetBlogPost(string id)
        {
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return Ok(blogPost);
        }

        //Code to get a Blog Post comments
        [Route("{id}/comments")]
        [HttpGet]
        public IHttpActionResult GetBlogPostComments(string id)
        {
            return Ok(db.BlogPosts.Find(id).Comments.ToList());
        }
        
        //Code to add a comment to a Blog Post
        [Route("{id}/comments")]
        [HttpPost]
        public IHttpActionResult PostBlogPostComment(string id, CommentViewModel viewModel)
        {
            Comment newComment = new Comment()
            {
                CommentId = Guid.NewGuid().ToString(),
                Content = viewModel.Content,
                PostUser = db.Users.Find(viewModel.PostUser).UserName
            };

            db.BlogPosts.Find(id).Comments.Add(newComment);
            db.SaveChanges();

            return Ok("Comment successfully added to the post");
        }

        // PUT: api/BlogPosts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBlogPost(string id, BlogPost blogPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != blogPost.BlogPostId)
            {
                return BadRequest();
            }

            db.Entry(blogPost).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogPostExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/BlogPosts
        [ResponseType(typeof(BlogPost))]
        public IHttpActionResult PostBlogPost(BlogPost blogPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            blogPost.BlogPostId = Guid.NewGuid().ToString();
            blogPost.URL = BaseUrl + "/blogposts/"+blogPost.BlogPostId;

            db.BlogPosts.Add(blogPost);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BlogPostExists(blogPost.BlogPostId))
                {
                    return Conflict();
                }
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = blogPost.BlogPostId }, blogPost);
        }

        // DELETE: api/BlogPosts/5
        [ResponseType(typeof(BlogPost))]
        public IHttpActionResult DeleteBlogPost(string id)
        {
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            db.BlogPosts.Remove(blogPost);
            db.SaveChanges();

            return Ok(blogPost);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BlogPostExists(string id)
        {
            return db.BlogPosts.Count(e => e.BlogPostId == id) > 0;
        }
    }
}