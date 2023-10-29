using CarRent.Database.Models;

namespace CarRent.Areas.Admin.ViewModels.Order
{
    public class OrderViewModel
    {
        public OrderViewModel(int ıd, DateTime? pickupDate, DateTime? dropDate, string? name,
            string? surname, string? email, string? phoneNumber, string brand, string model, int year, DateTime createdAt)
        {
            Id = ıd;
            PickupDate = pickupDate;
            DropDate = dropDate;
            Name = name;
            Surname = surname;
            Email = email;
            PhoneNumber = phoneNumber;
            Brand = brand;
            Model = model;
            Year = year;
            CreatedAt = createdAt;
        }

        public int Id { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? DropDate { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
