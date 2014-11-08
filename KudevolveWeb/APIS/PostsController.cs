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
        public string BaseUrl = "http://kudevolvelive.azurewebsites.net";
        RealTimePostUpdater signalr = RealTimePostUpdater.GetInstance();

        //Make a public object to send updates to All signalr Clients


        // GET: api/Posts
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetPosts()
        {
            return Ok(db.Posts.Include(p => p.Owner).Include(p => p.Comments).OrderByDescending(o => o.PostId).ToList());
        }


        // GET: api/Posts/5
        //Craft the Api request better
        [Route("{id}")]
        [HttpGet]
        [ResponseType(typeof(Post))]
        public IHttpActionResult GetPost(int id)
        {
            Post post = db.Posts.Where( p => p.PostId == id).Include( p => p.Comments).Include( p => p.Owner).FirstOrDefault();
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpGet]
        [Route("{id}/comments")]
        public List<Comment> GetPostComments(int id)
        {
            //Get the Comments from the post here
            return db.Posts.Where(pst => pst.PostId == id).Include(p => p.Comments).FirstOrDefault().Comments.ToList();

        }

        
        [Route("{id}/comments")]
        [HttpPost]
        //Code to add comment here as a POST request
        public IHttpActionResult PostComment(int id, KudevolveWeb.ViewModels.CommentViewModel comment)
        {
            //Serialize the string into a comment
            // var comment = JsonConvert.SerializeObject(commentContent);
            var psto = db.Posts.Where(pst => pst.PostId == id).Include(p => p.Comments).FirstOrDefault();

            if (psto == null)
            {
                return BadRequest("Post does not exist");
            }
            Comment newComment = new Comment();
            newComment.CommentId = Guid.NewGuid().ToString();
            newComment.Content = comment.Content;
            newComment.PostUser = comment.PostUser;

            try
            {
               
                psto.Comments.Add(newComment);
                db.Entry(psto).State = EntityState.Modified;
                db.SaveChanges();
              
                signalr.UpdatePostComment(id,newComment);
                return Ok("Comment addition successful");
            }
            catch (Exception)
            {

                return BadRequest("something happened");
            }
            

            
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
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPost(int id, Post post)
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
            //var user = new AppUser();
            var user = db.Users.Find(viewModel.Ownerid);
           // post.PostId = Guid.NewGuid().ToString();
            post.Owner = user;
            post.Content = viewModel.Content;
            //post.PostId = Guid.NewGuid().ToString();
            post.URL = BaseUrl + "/posts/" + post.PostId;
            post.DateCreated = DateTime.Today.ToString();
            // db.Posts.Add(post);


            try
            {
                db.Posts.Add(post);
                db.SaveChanges();
                //Send the post to all Signalr Connections
                signalr.UpdatePost(post);
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

        private bool PostExists(int id)
        {
            return db.Posts.Count(e => e.PostId == id) > 0;
        }
    }
}