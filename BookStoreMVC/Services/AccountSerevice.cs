﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BookStoreMVC.Data;
using BookStoreMVC.Helper;
using BookStoreMVC.Models;
using BookStoreMVC.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BookStoreMVC.Services
{
    public class AccountService
    {
        private readonly AppDbContext _appDbContext;
        private readonly HttpContext _httpContext;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public AccountService(AppDbContext appDbContext,
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

        public async Task<CustomErrorCodes.AccountErrors> Login(LoginViewModel loginViewModel)
        {
            try
            {
                var user = _userManager.FindByEmailAsync(loginViewModel.Email).Result;
                if (user == null)
                {
                    return CustomErrorCodes.AccountErrors.UserNotFound;
                }

                var result =
                    _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);
                if (result.Result.Succeeded) return CustomErrorCodes.AccountErrors.Ok;
                return CustomErrorCodes.AccountErrors.PasswordIsWrong;
            }
            catch (Exception ex)
            {
                return CustomErrorCodes.AccountErrors.Exception;
            }
            
        }

        // todo: logout

        public async Task<User> Register(RegisterViewModel registerViewModel)
        {
            try
            {
                var RegisterUser = registerViewModel.ToUser();
                var userCreateResult = await _userManager.CreateAsync(RegisterUser,registerViewModel.Password);

                if (userCreateResult.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(registerViewModel.Email);
                        return user;
                }
            }
            catch (Exception e)
            {
            }

            return null;
        }

        private void Init()
        {
        }
    }
}