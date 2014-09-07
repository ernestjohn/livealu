using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using KudevolveWeb.Models;

namespace KudevolveWeb.APIS
{
    [RoutePrefix("api/v1/petitions")]
    public class PetitionsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Petitions
        [Route("")]
        [HttpGet]
        public IQueryable<Petition> GetPetitions()
        {
            return db.Petitions.Include(pe => pe.Owner);
        }

        // GET: api/Petitions/5
        [HttpGet]
        [Route("{id}")]
        [ResponseType(typeof(Petition))]
        public IHttpActionResult GetPetition(string id)
        {
            Petition petition = db.Petitions.Find(id);
            if (petition == null)
            {
                return NotFound();
            }

            return Ok(petition);
        }

        //Code to vote a petition
        [Route("{id}/vote/{userid}")]
        [HttpGet]
        public IHttpActionResult VotePetition(string id, string userid)
        {
            //first check whether the petition has been voted
            var petition = db.Petitions.Find(id);
            var user = db.Users.Find(userid);

            if (petition.Voters.Contains(user))
            {
                return BadRequest("You have already voted for this petition");
            }
            else
            {
                db.Petitions.Find(id).Voters.Add(user);
                db.Petitions.Find(id).Votes += 1;
                db.SaveChanges();

                return Ok("Vote Successful");
            }
        }

        

        //Code to unvote petition
        [Route("{id}/vote/{userid}")]
        [HttpDelete]
        public IHttpActionResult UnvotePetition(string id, string userid)
        {
            //first check whether the petition has been voted
            var petition = db.Petitions.Find(id);
            var user = db.Users.Find(userid);

            if (petition.Voters.Contains(user))
            {
                return BadRequest("You had not voted for this petition");
            }
            else
            {
                db.Petitions.Find(id).Voters.Remove(user);
                db.Petitions.Find(id).Votes -= 1;
                db.SaveChanges();

                return Ok("Vote Successful");
            }
        }

        //Code to get petitions by order of votes


        //Code to pass a Petition

        //Code to get a passed petition

        //Code to unpass a petition


        // PUT: api/Petitions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPetition(string id, Petition petition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != petition.PetitionId)
            {
                return BadRequest();
            }

            db.Entry(petition).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetitionExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        //Code to make a Petrition from a POST
        [HttpGet]
        public IHttpActionResult MakePetitionFromPost(string postid)
        {
            var post = db.Posts.Find(postid);
            //Algorithm to make the post into a petition
            return Ok();
        }

        // POST: api/Petitions
        [ResponseType(typeof(Petition))]
        public IHttpActionResult PostPetition(Petition petition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Petitions.Add(petition);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PetitionExists(petition.PetitionId))
                {
                    return Conflict();
                }
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = petition.PetitionId }, petition);
        }

        // DELETE: api/Petitions/5
        [ResponseType(typeof(Petition))]
        public IHttpActionResult DeletePetition(string id)
        {
            Petition petition = db.Petitions.Find(id);
            if (petition == null)
            {
                return NotFound();
            }

            db.Petitions.Remove(petition);
            db.SaveChanges();

            return Ok(petition);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PetitionExists(string id)
        {
            return db.Petitions.Count(e => e.PetitionId == id) > 0;
        }
    }
}