namespace CarRent.Areas.Admin.ViewModels.Slider
{
    public class SliderListViewModel
    {
        public SliderListViewModel(int ıd, string? smallHeader, string? header, string? tittle, string ımageUrl)
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
        public string ImageUrl { get; set; }
 
    }
}
