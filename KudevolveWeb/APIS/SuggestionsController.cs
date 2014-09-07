using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using KudevolveWeb.Models;

namespace KudevolveWeb.APIS
{
    //Remember to add the route prefix here [RoutePrefix]
    [RoutePrefix("api/v1/suggestions")]
    public class SuggestionsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public string BaseUrl = "http://kudevolve.azurewebsites.net";

        // GET: api/Suggestions
        public IQueryable<Suggestion> GetSuggestions()
        {
            return db.Suggestions;
        }

        // GET: api/Suggestions/5
        [ResponseType(typeof(Suggestion))]
        [Route("{id}")]
        public IHttpActionResult GetSuggestion(string id)
        {
            Suggestion suggestion = db.Suggestions.Find(id);
            if (suggestion == null)
            {
                return NotFound();
            }

            return Ok(suggestion);
        }

        //Code to get the comments of a suggestion
        [Route("{id}/comments")]
        [HttpGet]
        public List<Comment> GetSuggestionComments(string id)
        {
            var suggestion = db.Suggestions.Find(id);
            if (suggestion == null)
            {
                return null;
            }
            return suggestion.Comments.ToList();

        }

        //Code to Post a comment to a suggestion - A POST request
        [Route("{id}/comments")]
        [HttpPost]
        public IHttpActionResult PostSuggestionComment(string id,Comment comment)
        {
            var suggestion = db.Suggestions.Find(id);
            if (suggestion == null)
            {
                return BadRequest();
            }
            db.Suggestions.Find(id).Comments.Add(comment);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }


        // PUT: api/Suggestions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSuggestion(string id, Suggestion suggestion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != suggestion.SuggestionId)
            {
                return BadRequest();
            }

            db.Entry(suggestion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuggestionExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Suggestions
        [ResponseType(typeof(Suggestion))]
        public IHttpActionResult PostSuggestion(Suggestion suggestion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            suggestion.SuggestionId = Guid.NewGuid().ToString();
            suggestion.URL = BaseUrl + "/suggestions/"+ suggestion.SuggestionId;
            db.Suggestions.Add(suggestion);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SuggestionExists(suggestion.SuggestionId))
                {
                    return Conflict();
                }
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = suggestion.SuggestionId }, suggestion);
        }

        // DELETE: api/Suggestions/5
        [ResponseType(typeof(Suggestion))]
        public IHttpActionResult DeleteSuggestion(string id)
        {
            Suggestion suggestion = db.Suggestions.Find(id);
            if (suggestion == null)
            {
                return NotFound();
            }

            db.Suggestions.Remove(suggestion);
            db.SaveChanges();

            return Ok(suggestion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SuggestionExists(string id)
        {
            return db.Suggestions.Count(e => e.SuggestionId == id) > 0;
        }
    }
}