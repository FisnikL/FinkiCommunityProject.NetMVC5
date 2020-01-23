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
using FinkiCommunity.Models;

namespace FinkiCommunity.Controllers
{
    public class GroupsApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //// GET: api/GroupsApi
        //public IQueryable<Group> GetGroups()
        //{
        //    return db.Groups;
        //}

        //// GET: api/GroupsApi/5
        //[ResponseType(typeof(Group))]
        //public IHttpActionResult GetGroup(int id)
        //{
        //    Group group = db.Groups.Find(id);
        //    if (group == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(group);
        //}

        //// PUT: api/GroupsApi/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutGroup(int id, Group group)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != group.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(group).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!GroupExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/GroupsApi
        //[ResponseType(typeof(Group))]
        //public IHttpActionResult PostGroup(Group group)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Groups.Add(group);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = group.Id }, group);
        //}

        // DELETE: api/GroupsApi/5
        [ResponseType(typeof(Group))]
        public IHttpActionResult DeleteGroup(int id)
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

        //private bool GroupExists(int id)
        //{
        //    return db.Groups.Count(e => e.Id == id) > 0;
        //}
    }
}