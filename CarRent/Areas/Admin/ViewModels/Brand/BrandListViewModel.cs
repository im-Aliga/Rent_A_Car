namespace CarRent.Areas.Admin.ViewModels.Brand
{
    public class BrandListViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public BrandListViewModel(int ıd, string name, string ımageUrl)
        {
            Id = ıd;
            Name = name;
            ImageUrl = ımageUrl;
        }
    }
}
