using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMVC.Data;
using BookStoreMVC.Helper;
using BookStoreMVC.Models;
using BookStoreMVC.Models.ViewModels;
using BookStoreMVC.Models.ViewModels.Ad;
using ImageMagick;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BookStoreMVC.Services
{
    public class AdService
    {
        private readonly AppDbContext _appDbContext;
        private readonly HttpContext _httpContext;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public AdService(AppDbContext appDbContext,
            IHttpContextAccessor httpContextAccessor,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager)
        {
            _appDbContext = appDbContext;
            _httpContext = httpContextAccessor.HttpContext;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<CustomErrorCodes.AdErrors> PostAd(PostAdViewModel postAdViewModel, User user)
        {
            var imagesRes = await UploadImages(postAdViewModel);
            if (imagesRes != CustomErrorCodes.AdErrors.Ok) return imagesRes;
            
            var book = new Ad();
            book.Author = postAdViewModel.Author;
            book.Conditions = postAdViewModel.AdCondition;
            book.Description = postAdViewModel.Description;
            book.Name = postAdViewModel.Name;
            book.Price = postAdViewModel.Price;
            book.User = user;
            book.GenresIds = postAdViewModel.SelectedGenres;
            book.SellOption = postAdViewModel.SellOption;
            book.UserId = user.Id;
            book.ImagePaths = postAdViewModel.imagePaths;
            book.UploadTime = DateTime.Now;
            var res = _appDbContext.Ads.Add(book).Entity;
            if (res == null)
            {
                foreach (var images in postAdViewModel.imagePaths)
                {
                    File.Delete(images);
                }
            }

            var dbRes = _appDbContext.SaveChanges();
            return CustomErrorCodes.AdErrors.Ok;
        }

        private async Task<CustomErrorCodes.AdErrors> UploadImages(PostAdViewModel postAdViewModel)
        {
            try
            {
                var images = postAdViewModel.Images.ToList();
                foreach (var image in images)
                {
                    if (!image.ContentType.Contains("image")) return CustomErrorCodes.AdErrors.FileTypeError;
                    if (image.Length >= 4000000 ) return CustomErrorCodes.AdErrors.FileSizeIsBig;
                }

                var imagesDirectory = Path.Combine("wwwroot", "BookImages");
                if (!Directory.Exists(imagesDirectory)) Directory.CreateDirectory(imagesDirectory);
                postAdViewModel.imagePaths = new List<string>();
                foreach (var image in images)
                {
                    postAdViewModel.imagePaths.Add(Path.Combine("BookImages",Guid.NewGuid().ToString()+Path.GetExtension(image.FileName)));
                    var path = Path.Combine("wwwroot",postAdViewModel.imagePaths.Last());
                    using (var stream = new FileStream(path, FileMode.Create))  
                    {  
                        await image.CopyToAsync(stream);  
                    }
                    var file = new FileInfo(path);

                    var optimizer = new ImageOptimizer()
                    {
                        OptimalCompression = true
                    };
                    optimizer.Compress(file);

                    file.Refresh();
                }

                return CustomErrorCodes.AdErrors.Ok;
            }
            catch (Exception e)
            {
                return CustomErrorCodes.AdErrors.FileTypeError;
            }
        }

        public async Task<List<AdViewModel>> Ads(int page = 1, FilterOption filterOption = FilterOption.NewToOld, int res = 8,int GenreId = 0)
        {
            List<Ad> allAds = null;
            switch (filterOption)
            {
                case FilterOption.HighToLow:
                {
                    allAds = _appDbContext.Ads.OrderByDescending(x => x.Price).ToList();
                    break;
                }
                case FilterOption.LowToHigh:
                {
                    allAds = _appDbContext.Ads.OrderBy(x => x.Price).ToList();
                    break;
                }
                case FilterOption.OldToNew:
                {
                    allAds = _appDbContext.Ads.OrderBy(x => x.UploadTime).ToList();
                    break;
                }
                case FilterOption.NewToOld:
                {
                    allAds = _appDbContext.Ads.OrderByDescending(x => x.UploadTime).ToList();
                    break;
                }
            }

            if (GenreId != 0)
            {
                allAds = allAds.Where(x => x.GenresIds.Contains(GenreId)).ToList();
            }
            var ads = allAds.Skip((page - 1) * res).Take(res);
            var result = ads.Select(x =>
                new AdViewModel()
                {
                    Author = x.Author,
                    Condition = x.Conditions,
                    Description = x.Description,
                    Genres = new List<Genre>(_appDbContext.Genres.Where(y=>x.GenresIds.Contains(y.Id))),
                    Name = x.Name,
                    Price = x.Price,
                    User = x.User,
                    ImagePaths = x.ImagePaths,
                    SellOption = x.SellOption,
                    UploadTime = x.UploadTime.ToShortDateString()
                }
            ).ToList();
            return result.ToList();
        }
    }
}