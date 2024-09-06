/// <summary>
    /// The Table class represents a table in the restaurant with properties for ID, capacity, cost, and reservation status. 
    /// The VipTable class inherits from Table and adds a special service property, 
    /// while the StandardTable class also inherits from Table and includes a property indicating if it is near a window.
/// </summary>


namespace RestaurantReservationSystem.Api.Models
{
    public class Table
    {
        public int TableId { get; set; }
        public int Capacity { get; set; }
        public int Cost { get; set; }
        public bool IsReserved { get; set; }
    }

    public class VipTable : Table
    {
        public string SpecialService { get; set; }
    }

    public class StandardTable : Table
    {
        public bool NearWindow { get; set; }
    }
}
