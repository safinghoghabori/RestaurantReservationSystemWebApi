/// <summary>
        /// The RestaurantController class handles various operations related to the restaurant, including managing customers,
        /// tables, and reservations. 
        /// It uses authorization policies to restrict certain actions to users with specific roles.
        
        /// GetCustomers        :   Retrieves the list of customers from the restaurant.
        /// AddCustomer         :   Adds a new customer to the restaurant. Requires the "RestaurantOwner" role.
        /// GetTables           :   Retrieves the list of tables from the restaurant. Requires the "RestaurantOwner" role.
        /// AddTable            :   Adds a new table to the restaurant. Requires the "RestaurantOwner" role.
        /// UpdateTable         :   Updates an existing table in the restaurant. Requires the "RestaurantOwner" role.
        /// DeleteTable         :   Deletes a table from the restaurant by its ID. Requires the "RestaurantOwner" role.
        /// GetReservations     :   Retrieves the list of reservations from the restaurant.
        /// MakeReservation     :   Makes a new reservation at the restaurant. Requires the "RestaurantOwner" role.
        /// UpdateReservation   :   Updates an existing reservation by its ID. Requires the "RestaurantOwner" role.
        /// CancelReservation   :   Cancels an existing reservation by its ID. Requires the "RestaurantOwner" role.
        /// UpdateCustomer      :   Updates an existing customer by their ID. Requires the "RestaurantOwner" role.
        /// DeleteCustomer      :   Deletes an existing customer by their ID. Requires the "RestaurantOwner" role.
/// </summary>


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationSystem.Api.Exceptions;
using RestaurantReservationSystem.Api.Models;

namespace RestaurantReservationSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private static readonly Restaurant _restaurant = new Restaurant { Name = "SunShine" };

        [HttpGet("customers")]
        [Authorize]
        [Authorize(Policy = "RequireOwnerOrCustomerRole")]
        public ActionResult<List<Customer>> GetCustomers()
        {
            return Ok(_restaurant.Customers);
        }

        [HttpPost("customers")]
        [Authorize(Policy = "RequireRestaurantOwnerRole")]
        public ActionResult AddCustomer([FromBody] Customer customer)
        {
            _restaurant.AddCustomer(customer);
            return Ok(new { response = "Customer added successfully." });
        }

        [HttpGet("tables")]
        [Authorize(Policy = "RequireOwnerOrCustomerRole")]
        public ActionResult<List<Table>> GetTables()
        {
            return Ok(_restaurant.Tables);
        }

        [HttpPost("tables")]
        [Authorize(Policy = "RequireRestaurantOwnerRole")]
        public ActionResult AddTable([FromBody] Table table)
        {
            _restaurant.AddTable(table);
            return Ok("Table added successfully.");
        }

        [HttpPut("tables")]
        [Authorize(Policy = "RequireRestaurantOwnerRole")]
        public ActionResult UpdateTable([FromBody] Table table)
        {
            try
            {
                _restaurant.UpdateTable(table);
                return Ok(new { message = "Table updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("tables/{id}")]
        [Authorize(Policy = "RequireRestaurantOwnerRole")]
        public ActionResult DeleteTable(int id)
        {
            _restaurant.DeleteTable(id);
            return Ok(new { message = "Table deleted successfully." });
        }

        [HttpGet("reservations")]
        [Authorize(Policy = "RequireOwnerOrCustomerRole")]
        public ActionResult<List<Reservation>> GetReservations()
        {
            return Ok(_restaurant.Reservations);
        }

        [HttpPost("reservations")]
        [Authorize(Policy = "RequireOwnerOrCustomerRole")]
        public ActionResult MakeReservation([FromBody] Reservation reservation)
        {
            try
            {
                _restaurant.AddReservation(reservation);
                return Ok(new { message = "Reservation made successfully." });
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
        [Authorize(Policy = "RequireRestaurantOwnerRole")]
        public ActionResult UpdateReservation(int id, [FromBody] Reservation updatedReservation)
        {
            try
            {
                _restaurant.UpdateReservation(id, updatedReservation);
                return Ok(new { message = "Reservation updated successfully." });
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
        [Authorize(Policy = "RequireRestaurantOwnerRole")]
        public ActionResult CancelReservation(int id)
        {
            try
            {
                _restaurant.CancelReservation(id);
                return Ok(new { message = "Reservation canceled successfully." });
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
        [HttpPut("customers/{id}")]
        [Authorize(Policy = "RequireRestaurantOwnerRole")]
        public ActionResult UpdateCustomer(int id, [FromBody] Customer customer)
        {
            try
            {
                _restaurant.UpdateCustomer(id, customer);
                return Ok("Customer updated successfully.");
            }
            catch (InvalidReservationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("customers/{id}")]
        [Authorize(Policy = "RequireRestaurantOwnerRole")]
        public ActionResult DeleteCustomer(int id)
        {
            try
            {
                _restaurant.DeleteCustomer(id);
                return Ok("Customer deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
