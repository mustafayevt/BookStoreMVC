using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMVC.Data;
using BookStoreMVC.Helper;
using BookStoreMVC.Models;
using BookStoreMVC.Models.ViewModels.Ad;
using BookStoreMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace BookStoreMVC.Controllers
{
    [Route("Ad")]
    [Authorize]
    public class AdsController : Controller
    {
        private readonly ILogger _logger;
        private readonly AccountService _accountService;
        private readonly AdService _adService;
        private readonly AppDbContext _appDbContext;
        private readonly HttpContext _httpContext;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public AdsController(
            ILogger<AccountController> logger,
            AccountService accountService,
            AdService adService,
            IHttpContextAccessor httpContextAccessor,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            AppDbContext appDbContext)
        {
            _logger = logger;
            _accountService = accountService;
            _adService = adService;
            _appDbContext = appDbContext;
            _httpContext = httpContextAccessor.HttpContext;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET
        [HttpGet]
        [Route("PostAd")]
        public async Task<IActionResult> PostAd()
        {
            var model = new PostAdViewModel();
            model.GetGenres(_appDbContext);

            return View(model);
        }

        [HttpPost]
        [Route("PostAd")]
        public async Task<IActionResult> PostAd(PostAdViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var postAdResult = _adService.PostAd(model, user).Result;
                if (postAdResult == CustomErrorCodes.AdErrors.FileTypeError)
                {
                    ModelState.AddModelError("","Yalnız Şəkil Seçin");
                    model.Images = new FormFileCollection();
                    model.GetGenres(_appDbContext);
                    return View(model);
                }
                if (postAdResult == CustomErrorCodes.AdErrors.FileSizeIsBig)
                {
                    ModelState.AddModelError("","Şəkil Ölçüsü 4MB - Dan Artıq Ola Bilməz");
                    model.Images = new FormFileCollection();
                    model.GetGenres(_appDbContext);
                    return View(model);
                }

                return View("Successful");
            }
            else
            {
                model.GetGenres(_appDbContext);
                return View(model);
            }
        }
    }
}