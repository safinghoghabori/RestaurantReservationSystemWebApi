using System;
/// <summary>
    /// The OverBookingException class is a custom exception used to handle scenarios where overbooking occurs in the restaurant reservation system.
    /// It inherits from the base Exception class and provides a constructor that accepts a custom error message. 
    /// OverBookingException Constructor: Initializes a new instance of the OverBookingException class with a specified error message.
/// </summary>


namespace RestaurantReservationSystem.Api.Exceptions
{
    public class OverBookingException : Exception
    {
        public OverBookingException(string message) : base(message) { }
    }
}
