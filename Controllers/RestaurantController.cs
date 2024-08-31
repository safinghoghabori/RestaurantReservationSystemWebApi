using Microsoft.AspNetCore.Mvc;
using RestaurantReservationSystem.Api.Exceptions;
using RestaurantReservationSystem.Api.Models;

namespace RestaurantReservationSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private static readonly Restaurant _restaurant = new Restaurant { Name = "SunShine" };

        [HttpGet("customers")]
        public ActionResult<List<Customer>> GetCustomers() => Ok(_restaurant.Customers);

        [HttpPost("customers")]
        public ActionResult AddCustomer([FromBody] Customer customer)
        {
            _restaurant.AddCustomer(customer);
            return Ok("Customer added successfully.");
        }

        [HttpGet("tables")]
        public ActionResult<List<Table>> GetTables() => Ok(_restaurant.Tables);

        [HttpPost("tables")]
        public ActionResult AddTable([FromBody] Table table)
        {
            _restaurant.AddTable(table);
            return Ok("Table added successfully.");
        }

        [HttpGet("reservations")]
        public ActionResult<List<Reservation>> GetReservations() => Ok(_restaurant.Reservations);

        [HttpPost("reservations")]
        public ActionResult MakeReservation([FromBody] Reservation reservation)
        {
            try
            {
                _restaurant.AddReservation(reservation);
                return Ok("Reservation made successfully.");
            }
            catch (InvalidReservationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (DoubleBookingException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (OverBookingException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut("reservations/{id}")]
        public ActionResult UpdateReservation(int id, [FromBody] Reservation updatedReservation)
        {
            try
            {
                _restaurant.UpdateReservation(id, updatedReservation);
                return Ok("Reservation updated successfully.");
            }
            catch (InvalidReservationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (DoubleBookingException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (OverBookingException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("reservations/{id}")]
        public ActionResult CancelReservation(int id)
        {
            try
            {
                _restaurant.CancelReservation(id);
                return Ok("Reservation canceled successfully.");
            }
            catch (InvalidReservationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
