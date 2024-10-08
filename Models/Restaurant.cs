using System.Linq;
using Microsoft.AspNetCore.Http.HttpResults;
using RestaurantReservationSystem.Api.Exceptions;

namespace RestaurantReservationSystem.Api.Models
{
    public class Restaurant
    {
        public string Name { get; set; }
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<Table> Tables { get; set; } = new List<Table>();
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();

        public void AddCustomer(Customer customer) => Customers.Add(customer);

        public void AddTable(Table table) => Tables.Add(table);

        public void UpdateTable(Table table)
        {
            var index = Tables.FindIndex(t => t.TableId == table.TableId);

            // Check if the table exists in the list
            if (index != -1)
            {
                // Update the table at the found index
                Tables[index] = table;
            }
            else
            {
                throw new Exception("Table not found.");
            }
        }

        public void DeleteTable(int id)
        {
            var table = Tables.FirstOrDefault(tab => tab.TableId == id);
            if (table != null)
            {
                Tables.Remove(table);
            }
        }

        public void AddReservation(Reservation reservation)
        {
            var table = Tables.FirstOrDefault(tab => tab.TableId == reservation.TableId);

            if (reservation.CustomerId == null || reservation.TableId == null || reservation.DateTime == default)
            {
                throw new InvalidReservationException("Reservation details are incomplete.");
            }
            if (Reservations.Any(r => r.TableId == reservation.TableId && r.DateTime == reservation.DateTime))
            {
                throw new OverBookingException("This table is already booked for the specified time.");
            }
            if (Reservations.Any(r => r.CustomerId == reservation.CustomerId && r.DateTime == reservation.DateTime))
            {
                throw new DoubleBookingException("Customer already has a reservation at the specified time.");
            }

            table.IsReserved = true;
            Reservations.Add(reservation);
        }

        public void UpdateReservation(int reservationId, Reservation updatedReservation)
        {
            var reservation = Reservations.FirstOrDefault(r => r.ReservationId == reservationId);
            if (reservation == null)
            {
                throw new InvalidReservationException("Reservation not found.");
            }

            reservation.DateTime = updatedReservation.DateTime;
            reservation.CustomerId = updatedReservation.CustomerId;
            reservation.TableId = updatedReservation.TableId;
        }
        public void UpdateCustomer(int customerId, Customer updatedCustomer)
        {
            var customer = Customers.FirstOrDefault(c => c.CustomerId == customerId);
            if (customer == null)
            {
                throw new InvalidReservationException("Customer not found.");
            }

            customer.Name = updatedCustomer.Name;
            customer.PhoneNumber = updatedCustomer.PhoneNumber;
            customer.Age = updatedCustomer.Age;
            customer.Gender = updatedCustomer.Gender;

        }


        public void CancelReservation(int reservationId)

        {
            var reservation = Reservations.FirstOrDefault(r => r.ReservationId == reservationId);
            var table = Tables.FirstOrDefault(tab => tab.TableId == reservation.TableId);
            if (reservation == null)
            {
                throw new InvalidReservationException("Reservation not found.");
            }

            table.IsReserved = false;
            Reservations.Remove(reservation);
        }

        public void DeleteCustomer(int customerId)
        {
            var customer = Customers.FirstOrDefault(c => c.CustomerId == customerId);
            if (customer == null)
            {
                throw new InvalidReservationException("Customer not found.");
            }

            Customers.Remove(customer);
        }

    }
}
