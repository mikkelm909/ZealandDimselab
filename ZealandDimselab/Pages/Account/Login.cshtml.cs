using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandDimselab.Models;
using ZealandDimselab.Services;

namespace ZealandDimselab.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly UserService _userService;
        [BindProperty]
        public string Email { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }

        public string Message { get; set; }

        public LoginModel(UserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (await _userService.ValidateEmail(Email, Password))
            {
                var claimsIdentity = _userService.CreateClaimIdentity(Email);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToPage("../Index");
            }

            Message = "Invalid attempt";
            return Page();
        }
    }
}
