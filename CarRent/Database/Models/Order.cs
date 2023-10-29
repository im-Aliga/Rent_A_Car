namespace CarRent.Database.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? BarCode { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? DropDate { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? OrderCarId { get; set; }
        public Car? OrderCar { get; set; }
        public int Count { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
