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
    public class DiscussionsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Discussions
        public IQueryable<Discussion> GetDiscussions()
        {
            return db.Discussions.Include(d => d.Posts).Include(d => d.Owner);
        }

        // GET: api/Discussions/5
        [ResponseType(typeof(Discussion))]
        public IHttpActionResult GetDiscussion(string id)
        {
            Discussion discussion = db.Discussions.Find(id);
            if (discussion == null)
            {
                return NotFound();
            }

            return Ok(discussion);
        }

        // PUT: api/Discussions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDiscussion(string id, Discussion discussion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != discussion.DiscussionId)
            {
                return BadRequest();
            }

            db.Entry(discussion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscussionExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Discussions
        [ResponseType(typeof(Discussion))]
        public IHttpActionResult PostDiscussion(Discussion discussion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            discussion.DiscussionId = Guid.NewGuid().ToString();
            db.Discussions.Add(discussion);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DiscussionExists(discussion.DiscussionId))
                {
                    return Conflict();
                }
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = discussion.DiscussionId }, discussion);
        }

        // DELETE: api/Discussions/5
        [ResponseType(typeof(Discussion))]
        public IHttpActionResult DeleteDiscussion(string id)
        {
            Discussion discussion = db.Discussions.Find(id);
            if (discussion == null)
            {
                return NotFound();
            }

            db.Discussions.Remove(discussion);
            db.SaveChanges();

            return Ok(discussion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DiscussionExists(string id)
        {
            return db.Discussions.Count(e => e.DiscussionId == id) > 0;
        }
    }
}