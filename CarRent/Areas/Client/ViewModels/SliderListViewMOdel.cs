namespace CarRent.Areas.Client.ViewModels
{
    public class SliderListViewMOdel
    {

        public string SmallHeader { get; set; }
        public string Header { get; set; }
        public string Tittle { get; set; }
        public string ImageUrl { get; set; }
        public SliderListViewMOdel()
        {
            
        }
        public SliderListViewMOdel(string smallHeader, string header, string tittle)
        {
            SmallHeader = smallHeader;
            Header = header;
            Tittle = tittle;
        }

        public SliderListViewMOdel(string smallHeader, string header, string tittle, string ımageUrl)
        {
            SmallHeader = smallHeader;
            Header = header;
            Tittle = tittle;
            ImageUrl = ımageUrl;
        }
    }
}
