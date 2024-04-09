using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Login.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using Login.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;
using Login.Models;

namespace Pinterest.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AccountController(UserManager<AppUser> userManager,
			SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(AccountLoginVM model)
		{
			if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
			if (!ModelState.IsValid) return View(model);

			var existingUser = await _userManager.FindByNameAsync(model.Email);

			if (existingUser == null)
			{
				model.ErrorMessage = "Username or password is incorrect!";
				return View(model);
			}

			var result = await _signInManager.PasswordSignInAsync(existingUser, model.Password, model.RememberMe, false);

			if (!result.Succeeded)
			{
				model.ErrorMessage = "Username or password is incorrect!";
				return View(model);
			}

			return RedirectToAction("Index", "Home");
		}
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var newUser = new AppUser
            {
                Firstname = model.FirstName,
                Lastname = model.LastName,
                Email = model.Email,
                UserName = model.Email,
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                model.ErrorMessage = string.Join(" ", result.Errors.Select(x => x.Description));
                return View(model);
            }

            Console.WriteLine(result.ToString());
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LogOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}
	}
}