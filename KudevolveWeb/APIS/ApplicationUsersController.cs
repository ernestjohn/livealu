using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using KudevolveWeb.Models;
using System;
using KudevolveWeb.RealTime;

namespace KudevolveWeb.APIS
{
    //First set the route prefix
    [RoutePrefix("api/v1/users")]
    public class ApplicationUsersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public List<AppUser> Users = new List<AppUser>();

        //The Global object of Signalr User Connections
        public UserConnections connectionManager = new UserConnections();

        // GET: api/ApplicationUsers
       
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetIdentityUsers()
        {
            foreach (var usera in db.Users)
            {
                    
                var use = new AppUser
                {
                    UserName = usera.UserName,
                    Id = usera.Id,
                    FirstName = usera.FirstName,
                    SecondName = usera.SecondName,
                    Email = usera.Email,
                    PhoneNumber = usera.PhoneNumber,
                    ImageUrl= usera.ImageUrl,
                    DateOfBirth = usera.DateOfBirth
                    
                };
                Users.Add(use);
            }
            return Ok(Users);
        }
        
        //Code to get Signalr User connections
        [Route("{userid}/signalrconnections")]
        [HttpGet]
        public IHttpActionResult GetUserConnections(string userid)
        {
            var connections = connectionManager.GetConnections(userid);

            if (connections != null)
            {
                return Ok(connections);
            }
            else
           
              return BadRequest("No user connections exist unfortunately");
            
        }

        //Code to post a user Signalr Connection
        [Route("{userid}/signalrconnections")]
        [HttpPost]
        public IHttpActionResult PostUserConnection(string userid, string connectionid)
        {
            try
            {
                connectionManager.AddUserConnection(userid, connectionid);
                return Ok("Connection addition Successful");
            }
            catch (Exception)
            {

                return BadRequest();
            }
            
        }

        //Code to get the user's messages from the Messaging Queue backplane
        ///

        [Route("{id}/messages")]
        [HttpGet]
        public IHttpActionResult GetUserMessages(string id)
        {
            return Ok();
        }

        //Code to register a use
        [Route("register")]
        [HttpPost]
        public IHttpActionResult RegisterUser(RegisterViewModel viewModel)
        {
            AppUser newUser = new AppUser()
            {
                 Id = Guid.NewGuid().ToString(),
                 County = viewModel.County,
                 DateOfBirth = viewModel.DateOfBirth,
                 Email = viewModel.Email,
                 FirstName = viewModel.FirstName,
                 ImageUrl = viewModel.Image,
                 Password  = viewModel.Password,
                 PhoneNumber = viewModel.PhoneNumber,
                 UserName = viewModel.UserName,
                 SecondName = viewModel.SecondName
            };

            db.Users.Add(newUser);
            db.SaveChanges();

            return Ok(newUser);
        }

        [Route("register")]
        [HttpGet]
        public IHttpActionResult RegisterSample()
        {
            RegisterViewModel viewModel = new RegisterViewModel();
            return Ok(viewModel);
        }


        [Route("login")]
        [HttpGet]
        public IHttpActionResult LoginSample()
        {
            LoginViewModel viewModel = new LoginViewModel();
            return Ok(viewModel);
        }

        [Route("login")]
        [HttpPost]
        public IHttpActionResult LoginUser(LoginViewModel viewModel)
        {
            var user = db.Users.Where(usr => usr.Email == viewModel.Email && usr.Password == viewModel.Password);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest("Invalid Username or Password, maybe the user does not exist. Kudevolve API");
            }
        }

        //Code to get a person's posts
        [Route("{id}/posts")]
        [HttpGet]
        public IHttpActionResult GetUserPosts(string id)
        {
            return Ok(db.Users.Find(id).Posts);
        }

        //Code to get Posts that a person follows

        [Route("{id}/postsifollow")]
        [HttpGet]
        public IHttpActionResult GetPostsIFollow(string id)
        {
            List<Post> myPosts = new List<Post>();

            //Query through all the Posts
            foreach (var post in db.Posts)
            {
                if (post.Followers.Where(user => user.Id == id).Count() > 0)
	                {
                        myPosts.Add(post);
	                }
                
            }

            return Ok(myPosts);
        }

        //Code to get a person's petitions
        [Route("{id}/petitions")]
        [HttpGet]
        public IHttpActionResult GetUserPetitions(string id)
        {
            return Ok(db.Users.Find(id).Petitions);
        }

        //code to get a person's groups
        [Route("{id}/groups")]
        [HttpGet]
        public IHttpActionResult GetUserGroups(string id)
        {
            return Ok(db.Users.Find(id).Groups);
        }

        

        //Get the user with the route api/user/id
        [Route("{id}")]
        // GET: api/ApplicationUsers/5
        [ResponseType(typeof(AppUser))]
        public IHttpActionResult GetApplicationUser(string id)
        {
            var applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return Ok(applicationUser);
        }

        //Code to search a user
        [Route("search/{name}")]
        [HttpPost]
        public IHttpActionResult SearchUser(string name)
        {
            var results = db.Users.Where(usr => usr.FirstName.Contains(name) || usr.SecondName.Contains(name));

            if (results.Count != 0)
            {
                return Ok(results);
            }
            return NotFound();
            
        }

        [Route("{id}/friends")]
        [HttpGet]
        public List<AppUser> GetUserFriends(string id)
        {
            AppUser user = await db.Users.FirstOrDefaultAsync(usr => usr.Id == id);
            var friends = new List<AppUser>();
            if (user != null)
                foreach (var friendz in user.Friends)
                {
                    var use = new AppUser
                    {
                        UserName = friendz.UserName,
                        Id = friendz.Id,
                        FirstName = friendz.FirstName,
                        SecondName = friendz.SecondName,
                        Email = friendz.Email,
                        PhoneNumber = friendz.PhoneNumber,
                        ImageUrl = friendz.ImageUrl,
                        DateOfBirth = friendz.DateOfBirth

                    };
                    friends.Add(use);
                }
            return friends;
        }

        //Api Action to add friend as a POST request
        [Route("{id}/friends")]
        [HttpPost]
        public IHttpActionResult PostUserFriend(FrienderViewModel friender)
        {
            //Get the User and add the friend
            var friend = db.Users.Find(friender.FriendId);
            if (friend == null)
            {
                return BadRequest();
            }
            db.Users.Find(friender.UserId).Friends.Add(friend);
            db.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        //Code to delete friends from a user
        [Route("{id}/friends/{friendid}")]
        [HttpGet]
        public IHttpActionResult DeleteUserFriend(string userid, string friendid)
        {
            var friend = db.Users.Find(friendid);
            if (friend == null)
            {
                return BadRequest();
            }
            db.Users.Find(userid).Friends.Remove(friend);

            db.SaveChanges();
            return Ok("Friend successfully removed");
        }

        // PUT: api/ApplicationUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutApplicationUser(string id, AppUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != applicationUser.Id)
            {
                return BadRequest();
            }

            db.Entry(applicationUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationUserExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ApplicationUsers
        [ResponseType(typeof(AppUser))]
        public IHttpActionResult PostApplicationUser(AppUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(applicationUser);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ApplicationUserExists(applicationUser.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = applicationUser.Id }, applicationUser);
        }

        // DELETE: api/ApplicationUsers/5
        [Route("{id}")]
        [HttpDelete]
        [ResponseType(typeof(AppUser))]
        public IHttpActionResult DeleteApplicationUser(string id)
        {
            var applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            db.Users.Remove(applicationUser);
            db.SaveChanges();

            return Ok(applicationUser);
        }
        


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApplicationUserExists(string id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}