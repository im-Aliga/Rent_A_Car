namespace CarRent.Areas.Client.ViewModels
{
    public class BrandViewModel
    {
        public BrandViewModel(int ıd, string name, string? ımageUrl)
        {
            Id = ıd;
            Name = name;
            ImageUrl = ımageUrl;
        }
        public BrandViewModel()
        {
            
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        
    }
}
