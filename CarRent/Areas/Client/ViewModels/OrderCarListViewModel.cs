namespace CarRent.Areas.Client.ViewModels
{
    public class OrderCarListViewModel
    {
        public List<int> CarIds { get; set; }
        public List<OrderCarViewModel> Cars { get; set; }
    }
}
