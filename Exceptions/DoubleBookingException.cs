 using System;

 /// <summary>
    /// The DoubleBookingException class is a custom exception used to handle scenarios where a double booking occurs in the restaurant reservation system.
    /// It inherits from the base Exception class and provides a constructor that accepts a custom error message.
    /// DoubleBookingException Constructor:  Initializes a new instance of the DoubleBookingException class with a specified error message.
/// </summary>
namespace RestaurantReservationSystem.Api.Exceptions
{
    public class DoubleBookingException : Exception
    {
        public DoubleBookingException(string message) : base(message) { }
    }
}
