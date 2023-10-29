namespace CarRent.Areas.Admin.ViewModels.About
{
    public class AboutViewModel
    {
        public AboutViewModel(int id, string? smallHeader, string? header, string? tittle)
        {
            Id = id;
            SmallHeader = smallHeader;
            Header = header;
            Tittle = tittle;
           
        }

        public AboutViewModel()
        {
            
        }

        public AboutViewModel(int ıd, string? smallHeader, string? header, string? tittle, string? ımageUrl, IFormFile ımage)
        {
            Id = ıd;
            SmallHeader = smallHeader;
            Header = header;
            Tittle = tittle;
            ImageUrl = ımageUrl;
            Image = ımage;
        }

        public AboutViewModel(int ıd, string? smallHeader, string? header, string? tittle, string? ımageUrl)
        {
            Id = ıd;
            SmallHeader = smallHeader;
            Header = header;
            Tittle = tittle;
            ImageUrl = ımageUrl;
        }

        public int Id { get; set; }
        public string? SmallHeader { get; set; }
        public string? Header { get; set; }
        public string? Tittle { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }
    }
}
