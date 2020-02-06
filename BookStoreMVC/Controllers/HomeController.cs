using System.Linq;
using BookStoreMVC.Data;
using BookStoreMVC.Models;
using BookStoreMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace BookStoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly AccountService _accountService;
        private readonly AdService _adService;
        private readonly AppDbContext _appDbContext;
        private readonly HttpContext _httpContext;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public HomeController(
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
        public IActionResult Index(int page = 1)
        {
            ViewData["Ads"] = _adService.Ads(page).Result;
            var pageCount = (double)_appDbContext.Ads.Count() / 8;
            pageCount = pageCount % 2 != 0 ? pageCount + 1 : pageCount;
            ViewData["PageCount"] = (int) pageCount;
            ViewData["Currentpage"] = page;
            ViewData["AllAdsCount"] = _appDbContext.Ads.Count();
            return View();
        }

        [Route("/Error")]
        [HttpGet]
        public IActionResult Error()
        {
            return View("Error");
        }
    }
}