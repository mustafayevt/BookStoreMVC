using System.ComponentModel.DataAnnotations;

namespace BookStoreMVC.Helper
{
    public class AdOptions
    {
        public enum AdCondition
        {
            [Display(Name="Yeni")]
            New,
            [Display(Name="İkinci Əl")]
            Used
        }

        public enum SellOption
        {
            [Display(Name = "Barter")]
            Change,
            [Display(Name = "Satış")]
            Sell,
            [Display(Name = "Kirayə")]
            Rent
        }
    }
}