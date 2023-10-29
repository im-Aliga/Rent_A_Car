namespace CarRent.Areas.Admin.ViewModels.Product
{
    public class ProductListViewModel
    {
        public ProductListViewModel(int id, string brand, string model,
            int year, string fuelType, int seatCount, string gearBox,
            float weeklyPrice, float dailyPrice, string ımageUrl)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Year = year;
            FuelType = fuelType;
            SeatCount = seatCount;
            GearBox = gearBox;
            WeeklyPrice = weeklyPrice;
            DailyPrice = dailyPrice;
            ImageUrl = ımageUrl;
        }

        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string FuelType { get; set; }
        public int SeatCount { get; set; }
        public string GearBox { get; set; }
        public float WeeklyPrice { get; set; }
        public float DailyPrice { get; set; }
        public string ImageUrl { get; set; }

    }
}
