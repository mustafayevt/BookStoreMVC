using System.Threading.Tasks;
using BookStoreMVC.Data;
using BookStoreMVC.Helper;
using BookStoreMVC.Models;
using BookStoreMVC.Models.ViewModels;
using BookStoreMVC.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace BookStoreMVC.Controllers
{
    [Route("Account")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ILogger _logger;
        private readonly AccountService _accountService;
        private readonly AppDbContext _appDbContext;
        private readonly HttpContext _httpContext;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        public AccountController(
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
        
        [HttpGet]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var loginResult = _accountService.Login(model).Result;
                if (loginResult == CustomErrorCodes.AccountErrors.Ok)
                {
                    return RedirectToPage("/");
                }
                else if (loginResult == CustomErrorCodes.AccountErrors.UserNotFound)
                {
                    ViewBag["error"] = "İstifadəçi Tapılmadı";
                }
                else if (loginResult == CustomErrorCodes.AccountErrors.PasswordIsWrong)
                {
                    ViewBag["error"] = "Şifrə Yalnışdır";
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}