using System.Collections.Generic;
using BookStoreMVC.Helper;
using BookStoreMVC.Migrations;
using BookStoreMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreMVC.Data
{
    public class AppDbContext:IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Ad> Ads { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        
    }
}