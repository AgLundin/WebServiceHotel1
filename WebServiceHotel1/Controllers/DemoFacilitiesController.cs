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
using WebServiceHotel1;

namespace WebServiceHotel1.Controllers
{
    public class DemoFacilitiesController : ApiController
    {
        private HotelContext db = new HotelContext();

        // GET: api/DemoFacilities
        public IQueryable<DemoFacilities> GetDemoFacilities()
        {
            return db.DemoFacilities;
        }

        // GET: api/DemoFacilities/5
        [ResponseType(typeof(DemoFacilities))]
        public IHttpActionResult GetDemoFacilities(int id)
        {
            DemoFacilities demoFacilities = db.DemoFacilities.Find(id);
            if (demoFacilities == null)
            {
                return NotFound();
            }

            return Ok(demoFacilities);
        }

        // PUT: api/DemoFacilities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDemoFacilities(int id, DemoFacilities demoFacilities)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != demoFacilities.Facility_ID)
            {
                return BadRequest();
            }

            db.Entry(demoFacilities).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DemoFacilitiesExists(id))
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

        // POST: api/DemoFacilities
        [ResponseType(typeof(DemoFacilities))]
        public IHttpActionResult PostDemoFacilities(DemoFacilities demoFacilities)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DemoFacilities.Add(demoFacilities);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DemoFacilitiesExists(demoFacilities.Facility_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = demoFacilities.Facility_ID }, demoFacilities);
        }

        // DELETE: api/DemoFacilities/5
        [ResponseType(typeof(DemoFacilities))]
        public IHttpActionResult DeleteDemoFacilities(int id)
        {
            DemoFacilities demoFacilities = db.DemoFacilities.Find(id);
            if (demoFacilities == null)
            {
                return NotFound();
            }

            db.DemoFacilities.Remove(demoFacilities);
            db.SaveChanges();

            return Ok(demoFacilities);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DemoFacilitiesExists(int id)
        {
            return db.DemoFacilities.Count(e => e.Facility_ID == id) > 0;
        }
    }
}