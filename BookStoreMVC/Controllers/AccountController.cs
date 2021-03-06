﻿using System.Threading;
using System.Threading.Tasks;
using BookStoreMVC.Data;
using BookStoreMVC.Helper;
using BookStoreMVC.Models;
using BookStoreMVC.Models.ViewModels;
using BookStoreMVC.Models.ViewModels.Account;
using BookStoreMVC.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
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
        public async Task<IActionResult> Login()
        {
            // Clear the existing external cookie to ensure a clean login process
            if(_signInManager.IsSignedIn(User))
            return RedirectToAction("SignOut");
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var loginResult = _accountService.Login(model).Result;
                if (loginResult == CustomErrorCodes.AccountErrors.Ok)
                {
                    return Redirect("/");
                }
                else if (loginResult == CustomErrorCodes.AccountErrors.UserNotFound)
                {
                    ModelState.AddModelError("UserNotFound","İstifadəçi Tapılmadı");
                }
                else if (loginResult == CustomErrorCodes.AccountErrors.PasswordIsWrong)
                {
                    ModelState.AddModelError("WrongPassword","Şifrə Yalnışdır");

                }
            }
            
            return View(model);
        }
        
        
        //register
        [HttpGet] 
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register()
        {
            // Clear the existing external cookie to ensure a clean login process
            //await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var registerResult = _accountService.Register(model).Result;
                if (registerResult == CustomErrorCodes.AccountErrors.Ok)
                {
                    return Redirect("/");
                }
                else if (registerResult == CustomErrorCodes.AccountErrors.EmailAlreadyExists)
                {
                    ModelState.AddModelError("EmailAlreadyExists","Bu Email Artıq Qeydiyyatdan Keçib");
                }
            }
            
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [Route("SignOut")]
        public async Task<IActionResult> SignOut()
        {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Login","Account");
            
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("ExternalUrl")]
        public async Task<IActionResult> ExternalUrl()
        {
            // return Redirect("/");
            
            var redirectUrl = Url.Action("Index", "Home", new {returnUrl = "/"});
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("google", redirectUrl);
            return new ChallengeResult("google",properties);
            
        }
    }
}