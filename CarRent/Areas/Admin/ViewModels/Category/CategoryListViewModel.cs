namespace CarRent.Areas.Admin.ViewModels.Category
{
    public class CategoryListViewModel
    {
        public CategoryListViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public CategoryListViewModel(int ıd, string name, string ımageUrl)
        {
            Id = ıd;
            Name = name;
            ImageUrl = ımageUrl;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
