﻿using System.Collections.Generic;
using BookStoreMVC.Helper;

namespace BookStoreMVC.Models.ViewModels.Ad
{
    public class AdViewModel
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public List<Genre> Genres { get; set; }
        public List<string> ImagePaths { get; set; }
        public AdOptions.AdCondition Condition { get; set; }
        public AdOptions.SellOption SellOption { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string UploadTime { get; set; }
        
    }
}