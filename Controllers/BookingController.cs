using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationSystem.Api.Models;

namespace RestaurantReservationSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private static readonly List<Booking> Bookings = new();
        private static int _nextId = 1;

        [HttpGet]
        [Authorize(Policy = "RequireOwnerOrCustomerRole")]
        public ActionResult<IEnumerable<Booking>> Get()
        {
            var userId = User.Identity.Name;
            var userRole = User.Claims.FirstOrDefault(c => c.Type == "Role")?.Value;

            if (userRole == "RestaurantOwner")
            {
                return Ok(Bookings);
            }

            return Ok(Bookings.Where(b => b.UserId == userId));
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "RequireOwnerOrCustomerRole")]
        public ActionResult<Booking> Get(int id)
        {
            var booking = Bookings.FirstOrDefault(b => b.Bid == id);
            if (booking == null)
            {
                return NotFound();
            }

            var userId = User.Identity.Name;
            var userRole = User.Claims.FirstOrDefault(c => c.Type == "Role")?.Value;

            if (userRole == "RestaurantOwner" || booking.UserId == userId)
            {
                return Ok(booking);
            }

            return Forbid();
        }

        [HttpPost]
        [Authorize(Policy = "RequireCustomerRole")]
        public ActionResult<Booking> Post([FromBody] Booking booking)
        {
            if (ModelState.IsValid)
            {
                booking.Bid = _nextId++;
                booking.UserId = User.Identity.Name;
                Bookings.Add(booking);
                return CreatedAtAction(nameof(Get), new { id = booking.Bid }, booking);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "RequireCustomerRole")]
        public ActionResult Put(int id, [FromBody] Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingBooking = Bookings.FirstOrDefault(b => b.Bid == id);
            if (existingBooking == null)
            {
                return NotFound();
            }

            var userId = User.Identity.Name;
            if (existingBooking.UserId != userId)
            {
                return Forbid();
            }

            existingBooking.Name = booking.Name;
            existingBooking.Age = booking.Age;
            existingBooking.Gender = booking.Gender;
            existingBooking.PhoneNumber = booking.PhoneNumber;
            existingBooking.Email = booking.Email;
            existingBooking.DateTime = booking.DateTime;
            existingBooking.Capacity = booking.Capacity;

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireCustomerRole")]
        public ActionResult Delete(int id)
        {
            var booking = Bookings.FirstOrDefault(b => b.Bid == id);
            if (booking == null)
            {
                return NotFound();
            }

            var userId = User.Identity.Name;
            if (booking.UserId != userId)
            {
                return Forbid();
            }

            Bookings.Remove(booking);
            return NoContent();
        }
    }
}
