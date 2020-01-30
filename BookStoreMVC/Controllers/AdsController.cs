using BookStoreMVC.Data;
using BookStoreMVC.Models;
using BookStoreMVC.Models.ViewModels.Ad;
using BookStoreMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookStoreMVC.Controllers
{
    [Route("Ad")]
    [Authorize]
    public class AdsController : Controller
    {
        private readonly ILogger _logger;
        private readonly AccountService _accountService;
        private readonly AppDbContext _appDbContext;
        private readonly HttpContext _httpContext;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        public AdsController(
            ILogger<AccountController> logger,
            AccountService accountService,
            IHttpContextAccessor httpContextAccessor,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            AppDbContext appDbContext)
        {
            _logger = logger;
            _accountService = accountService;
            _appDbContext = appDbContext;
            _httpContext = httpContextAccessor.HttpContext;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        // GET
        [HttpGet]
        [Route("PostAd")]
        public IActionResult PostAd()
        {
            return View();
        }
        [HttpPost]
        [Route("PostAd")]
        public IActionResult PostAd(PostAdViewModel model)
        {
            return null;
        }
    }
}