﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using KudevolveWeb.Models;
using System;
using KudevolveWeb.RealTime;
using KudevolveWeb.ViewModels;

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
                    DateOfBirth = usera.DateOfBirth,
                    County = usera.County
                   
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
            {
                return BadRequest("No user connections exist unfortunately");
            }
           
        }

        //Code to post a user Signalr Connection
        [Route("{userid}/signalrconnections")]
        [HttpPost]
        public IHttpActionResult PostUserConnection(string userid, SignalrConnection viewModel)
        {
            try
            {
                connectionManager.AddConnection(userid);
                connectionManager.AddUserConnection(viewModel.userid, viewModel.connectionid);
                return Ok("Connection addition Successful");
            }
            catch (Exception)
            {

                return BadRequest("Something went wrong adding the user connection");
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
                 //Instantiate a new empty user object
                 AppUser newUser = new AppUser();
                 
                 newUser.Id = Guid.NewGuid().ToString();
                 newUser.County = viewModel.County;
                 newUser.DateOfBirth = viewModel.DateOfBirth;
                 newUser.Email = viewModel.Email;
                 newUser.FirstName = viewModel.FirstName;
                 newUser.Password  = viewModel.Password;
                 newUser.PhoneNumber = viewModel.PhoneNumber;
                 newUser.UserName = viewModel.UserName;
                 newUser.SecondName = viewModel.SecondName;

                 try
                 {
                     //First check if the user already exists in the database with that email
                     if (db.Users.Count(u => u.Email == newUser.Email) > 0)
                     {
                         return BadRequest("Sorry!A user with the email already exists");
                     }
                     else
                     {
                         db.Users.Add(newUser);
                         db.SaveChanges();
                         return Ok(newUser);
                     }
                    
                 }
                 catch (Exception e)
                 {

                     return BadRequest(e.Message + " " + e.Source );
                 }
            
        }

        /*
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         */
        /// <summary>
        /// This is where I implement my facebook registration APIs
        /// </summary>
        /// <returns></returns>
        [Route("register/facebook/{id}")]
        [HttpPost]
        public IHttpActionResult RegisterUserFacebook(string id,RegisterViewModel viewModel)
        {
            AppUser newUser = new AppUser();

            newUser.Id = Guid.NewGuid().ToString();
            newUser.County = viewModel.County;
            newUser.DateOfBirth = viewModel.DateOfBirth;
            newUser.Email = viewModel.Email;
            newUser.FirstName = viewModel.FirstName;
            newUser.Password = viewModel.Password;
            newUser.PhoneNumber = viewModel.PhoneNumber;
            newUser.UserName = viewModel.UserName;
            newUser.SecondName = viewModel.SecondName;
            newUser.Facebook = id;

            try
            {
                db.Users.Add(newUser);
                db.SaveChanges();
                return Ok(newUser);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message + " " + e.Source);
            }

        }

        [Route("register/twitter/{id}")]
        [HttpPost]
        public IHttpActionResult RegisterUserTwitter(string id, RegisterViewModel viewModel)
        {
            AppUser newUser = new AppUser();

            newUser.Id = Guid.NewGuid().ToString();
            newUser.County = viewModel.County;
            newUser.DateOfBirth = viewModel.DateOfBirth;
            newUser.Email = viewModel.Email;
            newUser.FirstName = viewModel.FirstName;
            newUser.Password = viewModel.Password;
            newUser.PhoneNumber = viewModel.PhoneNumber;
            newUser.UserName = viewModel.UserName;
            newUser.SecondName = viewModel.SecondName;
            newUser.Twitter = id;

            try
            {
                db.Users.Add(newUser);
                db.SaveChanges();
                return Ok(newUser);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message + " " + e.Source);
            }

        }


        [Route("register/instagram/{id}")]
        [HttpPost]
        public IHttpActionResult RegisterUserInstagram(string id,RegisterViewModel viewModel)
        {
            AppUser newUser = new AppUser();

            newUser.Id = Guid.NewGuid().ToString();
            newUser.County = viewModel.County;
            newUser.DateOfBirth = viewModel.DateOfBirth;
            newUser.Email = viewModel.Email;
            newUser.FirstName = viewModel.FirstName;
            newUser.Password = viewModel.Password;
            newUser.PhoneNumber = viewModel.PhoneNumber;
            newUser.UserName = viewModel.UserName;
            newUser.SecondName = viewModel.SecondName;
            newUser.Instagram = id;

            try
            {
                db.Users.Add(newUser);
                db.SaveChanges();
                return Ok(newUser);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message + " " + e.Source);
            }

        }

        [Route("register/linkedin/{id}")]
        [HttpPost]
        public IHttpActionResult RegisterUserLinkedin(string id,RegisterViewModel viewModel)
        {
            AppUser newUser = new AppUser();

            newUser.Id = Guid.NewGuid().ToString();
            newUser.County = viewModel.County;
            newUser.DateOfBirth = viewModel.DateOfBirth;
            newUser.Email = viewModel.Email;
            newUser.FirstName = viewModel.FirstName;
            newUser.Password = viewModel.Password;
            newUser.PhoneNumber = viewModel.PhoneNumber;
            newUser.UserName = viewModel.UserName;
            newUser.SecondName = viewModel.SecondName;
            newUser.LinkedIn = id;

            try
            {
                db.Users.Add(newUser);
                db.SaveChanges();
                return Ok(newUser);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message + " " + e.Source);
            }

        }

        [Route("register/google/{id}")]
        [HttpPost]
        public IHttpActionResult RegisterUserGoogle(string id, RegisterViewModel viewModel)
        {
            AppUser newUser = new AppUser();

            newUser.Id = Guid.NewGuid().ToString();
            newUser.County = viewModel.County;
            newUser.DateOfBirth = viewModel.DateOfBirth;
            newUser.Email = viewModel.Email;
            newUser.FirstName = viewModel.FirstName;
            newUser.Password = viewModel.Password;
            newUser.PhoneNumber = viewModel.PhoneNumber;
            newUser.UserName = viewModel.UserName;
            newUser.SecondName = viewModel.SecondName;
            newUser.Google = id;

            try
            {
                db.Users.Add(newUser);
                db.SaveChanges();
                return Ok(newUser);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message + " " + e.Source);
            }

        }
        /*
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         */

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
            
            var user = db.Users.Where(usr => usr.Email == viewModel.Email && usr.Password == viewModel.Password)
                .Include(u => u.Groups).FirstOrDefault();

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest("Invalid Username or Password, maybe the user does not exist. Kudevolve API");
            }
        }

        //This is where I implement my social login APIs
        [Route("login/social/facebook")]
        [HttpPost]
        public IHttpActionResult FacebookLogin(FacebookLogin viewModel)
        {
            return Ok(db.Users.FirstOrDefault(u => u.Facebook == viewModel.identity));
        }

        [Route("login/social/twitter")]
        [HttpPost]
        public IHttpActionResult TwitterLogin(TwitterLogin viewmodel)
        {
            return Ok(db.Users.FirstOrDefault(u => u.Twitter == viewmodel.identity));
        }

        [Route("login/social/linkedin")]
        [HttpPost]
        public IHttpActionResult LinkedinLogin(LinkedinLogin viewModel)
        {
            return Ok(db.Users.FirstOrDefault(u => u.LinkedIn == viewModel.identity));
        }

        [Route("login/social/instagram")]
        [HttpPost]
        public IHttpActionResult InstagramLogin(InstagramLogin viewModel)
        {
            return Ok(db.Users.FirstOrDefault(u => u.Instagram == viewModel.identity));
        }

        [Route("login/social/google")]
        [HttpPost]
        public IHttpActionResult GoogleLogin(GoogleLogin viewModel)
        {
            return Ok(db.Users.FirstOrDefault(u => u.Google == viewModel.identity));
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
        [HttpGet]
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
            var results = db.Users.Where(usr => usr.FirstName.Contains(name) || usr.SecondName.Contains(name)).ToList();

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
            AppUser user =  db.Users.FirstOrDefault(usr => usr.Id == id);
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
        public IHttpActionResult PostUserFriend(string id,FrienderViewModel friender)
        {
            //Get the User and add the friend
            var friend = db.Users.Find(friender.FriendId);
            if (friend == null)
            {
                return BadRequest();
            }
            try
            {
                var user = db.Users.Find(friender.UserId);
                user.Friends.Add(friend);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return Ok("Friend Added Successful");
            }
            catch (Exception)
            {

                return BadRequest("Something happened");
            }
           
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