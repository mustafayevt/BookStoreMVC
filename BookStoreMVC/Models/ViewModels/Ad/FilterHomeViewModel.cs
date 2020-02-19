namespace BookStoreMVC.Models.ViewModels.Ad
{
    public class FilterHomeViewModel
    {
        public int page { get; set; } = 1;
        public FilterOption filterOption { get; set; } = FilterOption.NewToOld;
        public int GenreId { get; set; } = 0;
    }
}