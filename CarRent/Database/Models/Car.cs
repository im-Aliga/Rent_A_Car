namespace CarRent.Database.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string FuelType { get; set; }
        public int SeatCount { get; set; }
        public string GearBox { get; set; }
        public float WeeklyPrice { get; set; }
        public float DailyPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string? PhoteImageName { get; set; }
        public string? PhoteInFileSystem { get; set; }
    }
}
