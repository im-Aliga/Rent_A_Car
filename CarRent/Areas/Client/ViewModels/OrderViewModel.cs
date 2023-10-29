namespace CarRent.Areas.Client.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            
        }
        public OrderViewModel(int orderId)
        {
            OrderId = orderId;
        }

        public OrderViewModel(int orderId, string name, string surname, string email, string phoneNumber)
        {
            OrderId = orderId;
            Name = name;
            Surname = surname;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public int OrderId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class OrderCarViewModel
    {
        public OrderCarViewModel(int id, string brand, string model, int year)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Year = year;
        }

        public OrderCarViewModel()
        {
            
        }
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
    }
    public class OrderDateViewModel
    {

        public OrderDateViewModel()
        {
            
        }
        public OrderDateViewModel(int orderId)
        {
            OrderId=orderId;
        }

        public OrderDateViewModel(int orderId, DateTime pickup, DateTime drop)
        {
            OrderId = orderId;
            Pickup = pickup;
            Drop = drop;
        }

        public int OrderId { get; set; }
        public DateTime Pickup { get; set; }
        public DateTime Drop { get; set; }
    }
}
