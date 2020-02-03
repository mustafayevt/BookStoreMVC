using System.Collections.Generic;
using BookStoreMVC.Helper;

namespace BookStoreMVC.Models
{
    public class Ad
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public List<int> GenresIds { get; set; }
        public List<string> ImagePaths { get; set; }
        public AdOptions.AdCondition Conditions { get; set; }
        public AdOptions.SellOption SellOption { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}