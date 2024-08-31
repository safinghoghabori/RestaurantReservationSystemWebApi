using System;

namespace RestaurantReservationSystem.Api.Exceptions
{
    public class DoubleBookingException : Exception
    {
        public DoubleBookingException(string message) : base(message) { }
    }
}
