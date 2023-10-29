namespace CarRent.Areas.Admin.ViewModels.Category
{
    public class CategoryAddViewModel
    {
        public CategoryAddViewModel(int id, string name, int orderId)
        {
            Id = id;
            Name = name;
            OrderId = orderId;
        }

        public CategoryAddViewModel()
        {
            
        }

        public CategoryAddViewModel(int ıd, string name, int orderId, IFormFile? ımage, string? ımageUrl)
        {
            Id = ıd;
            Name = name;
            OrderId = orderId;
            Image = ımage;
            ImageUrl = ımageUrl;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderId { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
    }
}
