using System;

namespace RestaurantReservationSystem.Api.Exceptions
{
    public class OverBookingException : Exception
    {
        public OverBookingException(string message) : base(message) { }
    }
}
