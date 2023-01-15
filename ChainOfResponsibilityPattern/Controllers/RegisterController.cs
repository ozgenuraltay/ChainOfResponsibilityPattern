using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChainOfResponsibilityPattern.DAL.Entities;
using ChainOfResponsibilityPattern.Models;

namespace ChainOfResponsibilityPattern.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserRegisterVM userRegisterVM)
        {
            AppUser appUser = new AppUser()
            {
                Name = userRegisterVM.Name,
                Surname = userRegisterVM.Surname,
                UserName = userRegisterVM.Username,
                Email = userRegisterVM.Mail
            };

            var result = await _userManager.CreateAsync(appUser, userRegisterVM.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Login");

            }
            return View();
        }
    }
}
