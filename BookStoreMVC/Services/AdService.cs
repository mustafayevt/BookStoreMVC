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
                    if (image.Length >= 3000000 ) return CustomErrorCodes.AdErrors.FileSizeIsBig;
                }

                var imagesDirectory = Path.Combine("wwwroot", "BookImages");
                if (!Directory.Exists(imagesDirectory)) Directory.CreateDirectory(imagesDirectory);
                postAdViewModel.imagePaths = new List<string>();
                foreach (var image in images)
                {
                    postAdViewModel.imagePaths.Add(Path.Combine(imagesDirectory,Guid.NewGuid().ToString()+Path.GetExtension(image.FileName)));
                    var path = postAdViewModel.imagePaths.Last();
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
    }
}