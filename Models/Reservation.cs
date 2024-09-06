/// <summary>
    /// Represents a reservation with properties for ID, date and time, customer ID, and table ID.
/// </summary>

namespace RestaurantReservationSystem.Api.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime DateTime { get; set; }
        public int CustomerId { get; set; }
        public int TableId { get; set; }
    }
}
