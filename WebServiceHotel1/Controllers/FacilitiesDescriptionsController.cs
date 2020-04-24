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
    public class FacilitiesDescriptionsController : ApiController
    {
        private HotelContext db = new HotelContext();

        // GET: api/FacilitiesDescriptions
        public IQueryable<FacilitiesDescription> GetFacilitiesDescription()
        {
            return db.FacilitiesDescription;
        }

        // GET: api/FacilitiesDescriptions/5
        [ResponseType(typeof(FacilitiesDescription))]
        public IHttpActionResult GetFacilitiesDescription(int id)
        {
            FacilitiesDescription facilitiesDescription = db.FacilitiesDescription.Find(id);
            if (facilitiesDescription == null)
            {
                return NotFound();
            }

            return Ok(facilitiesDescription);
        }

        // PUT: api/FacilitiesDescriptions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFacilitiesDescription(int id, FacilitiesDescription facilitiesDescription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != facilitiesDescription.Id)
            {
                return BadRequest();
            }

            db.Entry(facilitiesDescription).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacilitiesDescriptionExists(id))
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

        // POST: api/FacilitiesDescriptions
        [ResponseType(typeof(FacilitiesDescription))]
        public IHttpActionResult PostFacilitiesDescription(FacilitiesDescription facilitiesDescription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FacilitiesDescription.Add(facilitiesDescription);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FacilitiesDescriptionExists(facilitiesDescription.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = facilitiesDescription.Id }, facilitiesDescription);
        }

        // DELETE: api/FacilitiesDescriptions/5
        [ResponseType(typeof(FacilitiesDescription))]
        public IHttpActionResult DeleteFacilitiesDescription(int id)
        {
            FacilitiesDescription facilitiesDescription = db.FacilitiesDescription.Find(id);
            if (facilitiesDescription == null)
            {
                return NotFound();
            }

            db.FacilitiesDescription.Remove(facilitiesDescription);
            db.SaveChanges();

            return Ok(facilitiesDescription);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FacilitiesDescriptionExists(int id)
        {
            return db.FacilitiesDescription.Count(e => e.Id == id) > 0;
        }
    }
}