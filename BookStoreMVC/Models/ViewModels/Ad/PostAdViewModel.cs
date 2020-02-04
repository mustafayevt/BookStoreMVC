using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BookStoreMVC.Data;
using BookStoreMVC.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStoreMVC.Models.ViewModels.Ad
{
    public class PostAdViewModel
    {
        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "Minimum 1 Şəkil Yüklənməlidir")]
        public IFormFileCollection Images { get; set; }

        [Required(ErrorMessage = "'Adı' Xanası Boş Qala Bilməz")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "'Yazıçı' Xanası Boş Qala Bilməz")]
        public string Author { get; set; }
        
        [Required(ErrorMessage = "'Qiymət' Xanası Boş Qala Bilməz")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public AdOptions.AdCondition AdCondition { get; set; }

        public AdOptions.SellOption SellOption { get; set; }
        
        [Required(ErrorMessage = "'Haqqında' Xanası Boş Qala Bilməz")]
        public string Description { get; set; }
 
        public Dictionary<int,SelectListGroup> SelectListGroups { get; set; }
        public List<SelectListItem> Genres { get; set; }
        
        [Required(ErrorMessage = "Janr Seçilməyib")]
        public List<int> SelectedGenres { get; set; }

        public List<string> imagePaths { get; set; }

        public void GetGenres(AppDbContext _appDbContext)
        {
            Genres = new List<SelectListItem>();
            var dbGenres = _appDbContext.Genres.ToList();

            var SelectListGroups = dbGenres.Where(x => x.ParentId == 0)
                .ToDictionary(y => y.Id,y=>new SelectListGroup(){Name = y.Name});
            
            foreach (var genre in dbGenres)
            {
                if (genre.ParentId != 0)
                {
                    Genres.Add(new SelectListItem()
                    {
                        Value = genre.Id.ToString(),
                        Text = genre.Name,
                        Group = SelectListGroups[genre.ParentId]
                    });
                }
            }

        }
    }
}