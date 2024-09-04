/// <summary>
    /// Represents a customer with properties for ID, name, phone number, age, and gender.
/// </summary>

namespace RestaurantReservationSystem.Api.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
    }
}
