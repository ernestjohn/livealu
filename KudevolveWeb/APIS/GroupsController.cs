using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using KudevolveWeb.Models;

namespace KudevolveWeb.APIS
{
    [RoutePrefix("api/v1/groups")]
    public class GroupsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Groups
        [HttpGet]
        [Route("")]
        public IQueryable<Group> GetGroups()
        {
            return db.Groups;
        }

        [HttpGet]
        [Route("{id}")]
        // GET: api/Groups/5
        [ResponseType(typeof(Group))]
        public IHttpActionResult GetGroup(string id)
        {
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return NotFound();
            }

            return Ok(group);
        }

        //Code to get the members of a group
        [HttpGet]
        [Route("{id}/members")]
        public IHttpActionResult GetGroupMembers(string id)
        {
            return Ok(db.Groups.Find(id).Members.ToList());
        }

        //Code to add a member to a group
        [Route("{id}/members/{usid}")]
        [HttpPost]
        public IHttpActionResult AddGroupMember(string id, string usid)
        {
            var user = db.Users.Find(usid);
            if (db.Groups.Find(id).Members.Contains(user))
            {
                return Conflict();
            }
            db.Groups.Find(id).Members.Add(user);
            db.SaveChanges();
            return Ok("User successfully added to the group");

           
        }

        // PUT: api/Groups/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGroup(string id, Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != group.Id)
            {
                return BadRequest();
            }

            db.Entry(group).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("create")]
        // POST: api/Groups
        [ResponseType(typeof(Group))]
        public IHttpActionResult PostGroup(Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Groups.Add(group);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (GroupExists(group.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = group.Id }, group);
        }

        [HttpDelete]
        [Route("{id}")]
        // DELETE: api/Groups/5
        [ResponseType(typeof(Group))]
        public IHttpActionResult DeleteGroup(string id)
        {
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return NotFound();
            }

            db.Groups.Remove(group);
            db.SaveChanges();

            return Ok(group);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GroupExists(string id)
        {
            return db.Groups.Count(e => e.Id == id) > 0;
        }
    }
}