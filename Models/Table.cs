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
