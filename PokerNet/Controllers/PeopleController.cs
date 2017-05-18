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
using PokerNet.Repository.Entities;
using PokerNet.Repository;
using PokerNet.Models;

namespace PokerNet.Controllers
{
    public class PeopleController : ApiController
    {
        private RepositoryContext db = new RepositoryContext();
        
        // GET: api/People
        public IList<Person> GetPeople()
        {
            return db.People.OrderBy(o=>o.Order).ToList();
        }
        
        // GET: api/People/5
        [ResponseType(typeof(Person))]
        public IHttpActionResult GetPerson(Guid id)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // PUT: api/People/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPerson(Guid id, Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.ID)
            {
                return BadRequest();
            }

            db.Entry(person).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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

        // POST: api/People
        [ResponseType(typeof(Person))]
        public IHttpActionResult PostPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.People.Add(person);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = person.ID }, person);
        }

        // DELETE: api/People/5
        [ResponseType(typeof(Person))]
        public IHttpActionResult DeletePerson(Guid id)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return NotFound();
            }

            db.People.Remove(person);
            db.SaveChanges();

            return Ok(person);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonExists(Guid id)
        {
            return db.People.Count(e => e.ID == id) > 0;
        }
    }
}