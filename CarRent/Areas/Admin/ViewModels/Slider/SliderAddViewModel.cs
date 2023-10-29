namespace CarRent.Areas.Admin.ViewModels.Slider
{
    public class SliderAddViewModel
    {
        public int Id { get; set; }
        public string? SmallHeader { get; set; }
        public string? Header { get; set; }
        public string? Tittle { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }
    }
}
