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
    [RoutePrefix("api/v1/articles")]
    public class ArticlesController : ApiController
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        public string BaseUrl = "http://kudevolve.azurewebsites.net";

        [Route("")]
        // GET: api/Articles
        public IQueryable<Article> GetArticles()
        {
            return db.Articles.Include(x => x.Owner);
        }

        [Route("{id}")]
        // GET: api/Articles/5
        [ResponseType(typeof(Article))]
        public IHttpActionResult GetArticle(string id)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
        }

        //Code to get the Articles comments
        [Route("{id}/comments")]
        [HttpGet]
        public List<Comment> GetArticleComments(string id)
        {
            var article = db.Articles.Find(id);
            if (article == null)
            {
                return null;
            }
            //Return the comments of the Article
            return article.Comments.ToList();
        }

        // PUT: api/Articles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArticle(string id, Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != article.ArticleId)
            {
                return BadRequest();
            }

            db.Entry(article).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Articles
        [ResponseType(typeof(Article))]
        public IHttpActionResult PostArticle(Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            article.ArticleId = Guid.NewGuid().ToString();
            article.URL = BaseUrl + "/article/"+article.ArticleId;
            db.Articles.Add(article);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ArticleExists(article.ArticleId))
                {
                    return Conflict();
                }
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = article.ArticleId }, article);
        }

        // DELETE: api/Articles/5
        [ResponseType(typeof(Article))]
        public IHttpActionResult DeleteArticle(string id)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return NotFound();
            }

            db.Articles.Remove(article);
            db.SaveChanges();

            return Ok(article);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArticleExists(string id)
        {
            return db.Articles.Count(e => e.ArticleId == id) > 0;
        }
    }
}