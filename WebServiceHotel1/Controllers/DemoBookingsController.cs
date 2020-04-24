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
    public class DemoBookingsController : ApiController
    {
        private HotelContext db = new HotelContext();

        // GET: api/DemoBookings
        public IQueryable<DemoBooking> GetDemoBooking()
        {
            return db.DemoBooking;
        }

        // GET: api/DemoBookings/5
        [ResponseType(typeof(DemoBooking))]
        public IHttpActionResult GetDemoBooking(int id)
        {
            DemoBooking demoBooking = db.DemoBooking.Find(id);
            if (demoBooking == null)
            {
                return NotFound();
            }

            return Ok(demoBooking);
        }

        // PUT: api/DemoBookings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDemoBooking(int id, DemoBooking demoBooking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != demoBooking.Booking_id)
            {
                return BadRequest();
            }

            db.Entry(demoBooking).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DemoBookingExists(id))
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

        // POST: api/DemoBookings
        [ResponseType(typeof(DemoBooking))]
        public IHttpActionResult PostDemoBooking(DemoBooking demoBooking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DemoBooking.Add(demoBooking);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = demoBooking.Booking_id }, demoBooking);
        }

        // DELETE: api/DemoBookings/5
        [ResponseType(typeof(DemoBooking))]
        public IHttpActionResult DeleteDemoBooking(int id)
        {
            DemoBooking demoBooking = db.DemoBooking.Find(id);
            if (demoBooking == null)
            {
                return NotFound();
            }

            db.DemoBooking.Remove(demoBooking);
            db.SaveChanges();

            return Ok(demoBooking);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DemoBookingExists(int id)
        {
            return db.DemoBooking.Count(e => e.Booking_id == id) > 0;
        }
    }
}