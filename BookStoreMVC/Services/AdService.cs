using System.Threading.Tasks;
using BookStoreMVC.Data;
using BookStoreMVC.Helper;
using BookStoreMVC.Models;
using BookStoreMVC.Models.ViewModels;
using BookStoreMVC.Models.ViewModels.Ad;
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


        public async Task<CustomErrorCodes.AdErrors> PostAd(PostAdViewModel postAdViewModel)
        {
            return CustomErrorCodes.AdErrors.Ok;
        }
    }
}