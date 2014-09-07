using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using KudevolveWeb.Models;
using KudevolveWeb.ViewModels;


namespace KudevolveWeb.APIS
{
    //Set the route Prefix to a custom value
    [RoutePrefix("api/v1/posts")]
    public class PostsController : ApiController
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();
        public string BaseUrl = "http://kudevolve.azurewebsites.net";

        //Make a public object to send updates to All signalr Clients
       

        // GET: api/Posts
        [Route("")]
        [HttpGet]
        public IQueryable<Post> GetPosts()
        {
            return db.Posts.Include(p => p.Owner).Include(p => p.Comments);
        }

        
        // GET: api/Posts/5
        //Craft the Api request better
        [Route("{id}")]
        [HttpGet]
        [ResponseType(typeof(Post))]
        public IHttpActionResult GetPost(string id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpGet]
        [Route("{id}/comments")]
        public List<Comment> GetPostComments(string id)
        {
            //Get the Comments from the post here
            var comments = db.Posts.Where(pst => pst.PostId == id).Single().Comments.ToList();
         
            return comments;

        }

        [HttpPost]
        [Route("{id}/comments")]
        //Code to add comment here as a POST request
        public IHttpActionResult PostComment(string id,Comment comment)
        {
            //Serialize the string into a comment
            // var comment = JsonConvert.SerializeObject(commentContent);
            var post = db.Posts.Find(id);
            if (post == null)
            {
                return BadRequest();
            }

            db.Posts.Find(id).Comments.Add(comment);
            db.SaveChanges();

            return Ok("Comment addition successful");
        }

        //Code to get Followers of a post
        [Route("{postid}/followers")]
        [HttpGet]
        public IHttpActionResult GetPostFollwowers(string postid)
        {
            List<AppUser> voters = new List<AppUser>();
            var post = db.Posts.Find(postid);
            foreach (var voter in post.Followers)
            {
                var user = db.Users.Find(voter.Id);
                voters.Add(user);
            }

            if (voters.Count != 0)
            {
                return Ok(voters);
            }
            else
            {
                return NotFound();
            }
          
            
        }

        //Code to follow a post
        [Route("{id}/follow/{userid}")]
        [HttpGet]
        public IHttpActionResult VotePost(string id, string userid)
        {
            var user = db.Users.Find(userid);
            //add the user as a post follower
            db.Posts.Find(id).Followers.Add(user);
            db.SaveChanges();

            return Ok("Successfully Followed the Post. Kudevolve API");
           
        }

        //Code to Unfollow a Post

        //Code to follow a post
        [Route("{id}/unfollow/{userid}")]
        [HttpGet]
        public IHttpActionResult UnVotePost(string id, string userid)
        {
            var user = db.Users.Find(userid);
            //add the user as a post follower
            db.Posts.Find(id).Followers.Remove(user);
            db.SaveChanges();

            return Ok("Successfully UnFollowed the Post. Kudevolve API");
           
        }

        // PUT: api/Posts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPost(string id, Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != post.PostId)
            {
                return BadRequest();
            }

            db.Entry(post).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Posts
        [Route("")]
        [ResponseType(typeof(Post))]
        [HttpPost]
        public IHttpActionResult PostPost(PostViewModel viewModel)
        {
            Post post = new Post();
            var user = db.Users.Find(viewModel.Ownerid);
            post.PostId = Guid.NewGuid().ToString();
            post.Owner = user;
            post.Content = viewModel.Content;
            post.PostId = Guid.NewGuid().ToString();
            post.URL = BaseUrl + "/posts/"+post.PostId;
            post.DateCreated = DateTime.Today.ToString();
            db.Posts.Add(post);
            

            try
            {
                db.SaveChanges();
                //Send the post to all Signalr Connections
                RealTimePostUpdater.UpdatePost(post);
            }
            catch (DbUpdateException)
            {
                return BadRequest("Unable to save the post or send the post to clients using Signalr. Sorry");
            }

            return Ok(post);
        }

        // DELETE: api/Posts/5
        [Route("{id}")]
        [ResponseType(typeof(Post))]
        [HttpDelete]
        public IHttpActionResult DeletePost(string id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }

            db.Posts.Remove(post);
            db.SaveChanges();

            return Ok(post);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostExists(string id)
        {
            return db.Posts.Count(e => e.PostId == id) > 0;
        }
    }
}