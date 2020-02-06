using System.Collections.Generic;
using System.Linq;
using BookStoreMVC.Helper;
using BookStoreMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreMVC.Data
{
    public class AppDbContext:IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
             Init();
        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Ad> Ads { get; set; }



        private void Init()
        {
            if (!Genres.Any())
            {
                Genres.Add(new Genre("Bədii"));
                SaveChanges();
                var fictionId = Genres.Last().Id;
                var fictions = new List<Genre>
                {
                    new Genre(fictionId, "Klassiklər"),
                    new Genre(fictionId, "Macəra"),
                    new Genre(fictionId, "Komiks"),
                    new Genre(fictionId, "Detektiv"),
                    new Genre(fictionId, "Fantastika"),
                    new Genre(fictionId, "Nağıl"),
                    new Genre(fictionId, "Satira"),
                    new Genre(fictionId, "Qısa Hekayə"),
                    new Genre(fictionId, "Elmi-Fantastika"),
                    new Genre(fictionId, "Qorxu")
                };
                Genres.AddRange(fictions);
                SaveChanges();
                
                Genres.Add(new Genre("Qeyri-Bədii"));
                SaveChanges();
                var nonFictionId = Genres.Last().Id;
                var nonFictions = new List<Genre>
                {
                    new Genre(nonFictionId, "Bioqrafiya"),
                    new Genre(nonFictionId, "Özünü İnkişaf"),
                    new Genre(nonFictionId, "Marketinq"),
                    new Genre(nonFictionId, "Programlaşdırma"),
                    new Genre(nonFictionId, "Dərslik")
                };
                Genres.AddRange(nonFictions);
                SaveChanges();
            }
        }
    }
}