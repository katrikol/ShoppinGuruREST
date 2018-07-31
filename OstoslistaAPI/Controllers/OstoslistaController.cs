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
using OstoslistaAPI.Models;

namespace OstoslistaAPI.Controllers
{
    public class OstoslistaController : ApiController
    {
        private TuoteEntities db = new TuoteEntities();

        // GET: api/Ostoslista
        public IQueryable<Ostoslista> GetOstoslista()
        {
            return db.Ostoslista;
        }

        // GET: api/Ostoslista/5
        [ResponseType(typeof(Ostoslista))]
        public IHttpActionResult GetOstoslista(int id)
        {
            Ostoslista ostoslista = db.Ostoslista.Find(id);
            if (ostoslista == null)
            {
                return NotFound();
            }

            return Ok(ostoslista);
        }

        // PUT: api/Ostoslista/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOstoslista(int id, Ostoslista ostoslista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ostoslista.ostoid)
            {
                return BadRequest();
            }

            db.Entry(ostoslista).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OstoslistaExists(id))
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

        // POST: api/Ostoslista
        [ResponseType(typeof(Ostoslista))]
        public IHttpActionResult PostOstoslista(Ostoslista ostoslista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ostoslista.Add(ostoslista);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ostoslista.ostoid }, ostoslista);
        }

        // DELETE: api/Ostoslista/5
        [ResponseType(typeof(Ostoslista))]
        public IHttpActionResult DeleteOstoslista(int id)
        {
            Ostoslista ostoslista = db.Ostoslista.Find(id);
            if (ostoslista == null)
            {
                return NotFound();
            }

            db.Ostoslista.Remove(ostoslista);
            db.SaveChanges();

            return Ok(ostoslista);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OstoslistaExists(int id)
        {
            return db.Ostoslista.Count(e => e.ostoid == id) > 0;
        }
    }
}