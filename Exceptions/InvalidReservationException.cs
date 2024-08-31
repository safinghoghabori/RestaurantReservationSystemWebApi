using System;

namespace RestaurantReservationSystem.Api.Exceptions
{
    public class InvalidReservationException : Exception
    {
        public InvalidReservationException(string message) : base(message) { }
    }
}
